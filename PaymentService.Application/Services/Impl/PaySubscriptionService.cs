using PaymentService.Application.DTOs.Payments;
using PaymentService.Application.Services.Abstractions;
using PaymentService.Domian.Entities;
using PaymentService.Domian.Enums;
using PaymentService.Domian.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.Application.Services.Impl
{
    public class PaySubscriptionService : IPaySubscriptionService
    {

        private readonly IUnitOfWork _unitOfWork;

        public PaySubscriptionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // إنشاء دفع كامل أو دفع أول قسط
        public async Task<Guid> CreatePaymentAsync(CreatePaymentDto dto, Guid createdBy)
        {
            if (dto.PaymentType == PaymentType.Cash && dto.Installments?.Any() == true)
                throw new InvalidOperationException("Cash payment cannot have installments.");

            if (dto.PaymentType == PaymentType.Installments && (dto.Installments == null || !dto.Installments.Any()))
                throw new InvalidOperationException("Installments are required.");

            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                SubscriptionId = dto.SubscriptionId,
                MemberId = dto.MemberId,
                TotalAmount = dto.TotalAmount,
                PaidAmount = dto.PaymentType == PaymentType.Cash ? dto.TotalAmount : dto.Installments!.First().Amount,
                RemainingAmount = dto.PaymentType == PaymentType.Cash ? 0 : dto.TotalAmount - dto.Installments!.First().Amount,
                Currency = dto.Currency,
                PaymentType = dto.PaymentType,
                Status = dto.PaymentType == PaymentType.Cash ? PaymentStatus.Paid : PaymentStatus.PartiallyPaid,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = createdBy
            };

            await _unitOfWork.Payments.AddAsync(payment);

            // تسجيل الأقساط
            if (dto.PaymentType == PaymentType.Cash)
            {
                var installment = new PaymentInstallment
                {
                    Id = Guid.NewGuid(),
                    PaymentId = payment.Id,
                    InstallmentNumber = 1,
                    Amount = payment.TotalAmount,
                    DueDate = payment.CreatedAt,
                    PaidAt = payment.CreatedAt,
                    Status = InstallmentStatus.Paid
                };
                await _unitOfWork.PaymentInstallments.AddAsync(installment);

                var transaction = new PaymentTransaction
                {
                    Id = Guid.NewGuid(),
                    PaymentId = payment.Id,
                    PaymentInstallmentId = installment.Id,
                    Amount = payment.TotalAmount,
                    Currency = payment.Currency,
                    Method = PaymentMethod.Cash,
                    Status = TransactionStatus.Success,
                    PaidAt = DateTime.UtcNow,
                    ReferenceNumber = "Cash-" + Guid.NewGuid()
                };
                await _unitOfWork.paymentTransactionRepository.AddAsync(transaction);
            }
            else
            {
                // دفع أول قسط فقط
                var firstDtoInstallment = dto.Installments!.First();
                var firstInstallment = new PaymentInstallment
                {
                    Id = Guid.NewGuid(),
                    PaymentId = payment.Id,
                    InstallmentNumber = 1,
                    Amount = firstDtoInstallment.Amount,
                    DueDate = firstDtoInstallment.DueDate,
                    PaidAt = DateTime.UtcNow,
                    Status = InstallmentStatus.Paid
                };
                await _unitOfWork.PaymentInstallments.AddAsync(firstInstallment);

                var transaction = new PaymentTransaction
                {
                    Id = Guid.NewGuid(),
                    PaymentId = payment.Id,
                    PaymentInstallmentId = firstInstallment.Id,
                    Amount = firstInstallment.Amount,
                    Currency = payment.Currency,
                    Method = dto.PaymentMethod,
                    Status = TransactionStatus.Success,
                    PaidAt = DateTime.UtcNow,
                    ReferenceNumber = "Installment-" + Guid.NewGuid()
                };
                await _unitOfWork.paymentTransactionRepository.AddAsync(transaction);

                // تسجيل باقي الأقساط كـ Pending
                int installmentNumber = 2;
                foreach (var dtoInst in dto.Installments.Skip(1))
                {
                    var installment = new PaymentInstallment
                    {
                        Id = Guid.NewGuid(),
                        PaymentId = payment.Id,
                        InstallmentNumber = installmentNumber++,
                        Amount = dtoInst.Amount,
                        DueDate = dtoInst.DueDate,
                        Status = InstallmentStatus.Pending
                    };
                    await _unitOfWork.PaymentInstallments.AddAsync(installment);
                }
            }

            await _unitOfWork.SaveChangesAsync();
            return payment.Id;
        }

        // دفع أي قسط لاحق
        public async Task<Guid> PayInstallmentAsync(Guid paymentId, Guid installmentId, decimal amount, PaymentMethod method, string referenceNumber)
        {
            var payment = await _unitOfWork.Payments.GetByIdAsync(paymentId)
                          ?? throw new KeyNotFoundException("Payment not found.");

            var installment = await _unitOfWork.PaymentInstallments.GetByIdAsync(installmentId)
                               ?? throw new KeyNotFoundException("Installment not found.");

            if (installment.Status == InstallmentStatus.Paid)
                throw new InvalidOperationException("Installment already paid.");

            if (amount != installment.Amount)
                throw new InvalidOperationException("Paid amount must match the installment amount.");

            installment.PaidAt = DateTime.UtcNow;
            installment.Status = InstallmentStatus.Paid;

            payment.PaidAmount += amount;
            payment.RemainingAmount -= amount;
            payment.Status = payment.RemainingAmount == 0 ? PaymentStatus.Paid : PaymentStatus.PartiallyPaid;

            var transaction = new PaymentTransaction
            {
                Id = Guid.NewGuid(),
                PaymentId = payment.Id,
                PaymentInstallmentId = installment.Id,
                Amount = amount,
                Currency = payment.Currency,
                Method = method,
                Status = TransactionStatus.Success,
                PaidAt = DateTime.UtcNow,
                ReferenceNumber = referenceNumber
            };
            await _unitOfWork.paymentTransactionRepository.AddAsync(transaction);

            await _unitOfWork.SaveChangesAsync();
            return transaction.Id;
        }

        // تفاصيل الدفع مع الأقساط والمدفوعات
        public async Task<PaymentDetailsDto> GetPaymentDetailsAsync(Guid paymentId)
        {
            var payment = await _unitOfWork.Payments.GetByIdAsync(paymentId)
                          ?? throw new KeyNotFoundException("Payment not found.");

            var installments = await _unitOfWork.PaymentInstallments.GetByPaymentIdAsync(paymentId);
            var transactions = await _unitOfWork.paymentTransactionRepository.GetByPaymentIdAsync(paymentId);

            return new PaymentDetailsDto
            {
                Id = payment.Id,
                SubscriptionId = payment.SubscriptionId,
                MemberId = payment.MemberId,
                TotalAmount = payment.TotalAmount,
                PaidAmount = payment.PaidAmount,
                RemainingAmount = payment.RemainingAmount,
                Currency = payment.Currency,
                PaymentType = payment.PaymentType,
                Status = payment.Status,
                CreatedAt = payment.CreatedAt,
                Installments = installments.Select(x => new PaymentInstallmentDto
                {
                    Id = x.Id,
                    InstallmentNumber = x.InstallmentNumber,
                    Amount = x.Amount,
                    DueDate = x.DueDate,
                    PaidAt = x.PaidAt,
                    Status = x.Status
                }).ToList(),
                Transactions = transactions.Select(x => new PaymentTransactionDto
                {
                    Id = x.Id,
                    PaymentId = x.PaymentId,
                    PaymentInstallmentId = x.PaymentInstallmentId,
                    Amount = x.Amount,
                    Currency = x.Currency,
                    Method = x.Method,
                    Status = x.Status,
                    PaidAt = x.PaidAt,
                    ReferenceNumber = x.ReferenceNumber
                }).ToList()
            };
        }

        // قائمة الأقساط غير المدفوعة لعضو معين
        public async Task<IEnumerable<PaymentInstallmentDto>> GetPendingInstallmentsAsync(Guid memberId)
        {
            var payments = await _unitOfWork.Payments.GetByMemberIdAsync(memberId);
            var pendingInstallments = new List<PaymentInstallment>();

            foreach (var payment in payments)
            {
                var installments = await _unitOfWork.PaymentInstallments.GetByPaymentIdAsync(payment.Id);
                pendingInstallments.AddRange(installments.Where(x => x.Status == InstallmentStatus.Pending));
            }

            return pendingInstallments.Select(x => new PaymentInstallmentDto
            {
                Id = x.Id,
                InstallmentNumber = x.InstallmentNumber,
                Amount = x.Amount,
                DueDate = x.DueDate,
                PaidAt = x.PaidAt,
                Status = x.Status
            });
        }

        // كشف جميع المعاملات المتعلقة بدفع معين
        public async Task<IEnumerable<PaymentTransactionDto>> GetPaymentTransactionsAsync(Guid paymentId)
        {
            var transactions = await _unitOfWork.paymentTransactionRepository.GetByPaymentIdAsync(paymentId);
            return transactions.Select(x => new PaymentTransactionDto
            {
                Id = x.Id,
                PaymentId = x.PaymentId,
                PaymentInstallmentId = x.PaymentInstallmentId,
                Amount = x.Amount,
                Currency = x.Currency,
                Method = x.Method,
                Status = x.Status,
                PaidAt = x.PaidAt,
                ReferenceNumber = x.ReferenceNumber
            });
        }
    }
}
