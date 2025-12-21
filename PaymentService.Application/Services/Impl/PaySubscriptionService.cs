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
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPaymentInstallmentRepository _installmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaySubscriptionService(
            IPaymentRepository paymentRepository,
            IPaymentInstallmentRepository installmentRepository,
            IUnitOfWork unitOfWork)
        {
            _paymentRepository = paymentRepository;
            _installmentRepository = installmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> CreatePaymentAsync(CreatePaymentDto dto, Guid createdBy)
        {
            // Validation: Cash payment must not have installments
            if (dto.PaymentType == PaymentType.Cash && dto.Installments?.Any() == true)
                throw new InvalidOperationException("Cash payment cannot have installments.");

            // Validation: Installments must exist
            if (dto.PaymentType == PaymentType.Installments &&
                (dto.Installments == null || !dto.Installments.Any()))
                throw new InvalidOperationException("Installments are required.");

            // Validation: Installments total must equal payment total
            if (dto.PaymentType == PaymentType.Installments)
            {
                var installmentsTotal = dto.Installments!.Sum(x => x.Amount);
                if (installmentsTotal != dto.TotalAmount)
                    throw new InvalidOperationException("Installments total must equal total amount.");
            }

            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                SubscriptionId = dto.SubscriptionId,
                MemberId = dto.MemberId,
                TotalAmount = dto.TotalAmount,
                Currency = dto.Currency,
                PaymentType = dto.PaymentType,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = createdBy,
                Status = dto.PaymentType == PaymentType.Cash
                    ? PaymentStatus.Paid
                    : PaymentStatus.Pending
            };

            await _paymentRepository.AddAsync(payment);

            // Cash payment → single paid installment
            if (dto.PaymentType == PaymentType.Cash)
            {
                var installment = new PaymentInstallment
                {
                    Id = Guid.NewGuid(),
                    PaymentId = payment.Id,
                    Amount = payment.TotalAmount,
                    DueDate = payment.CreatedAt,
                    PaidAt = payment.CreatedAt,
                    Status = InstallmentStatus.Paid
                };

                await _installmentRepository.AddAsync(installment);
            }
            else
            {
                foreach (var installmentDto in dto.Installments!)
                {
                    if (installmentDto.Amount <= 0)
                        throw new InvalidOperationException("Installment amount must be greater than zero.");

                    var installment = new PaymentInstallment
                    {
                        Id = Guid.NewGuid(),
                        PaymentId = payment.Id,
                        Amount = installmentDto.Amount,
                        DueDate = installmentDto.DueDate,
                        Status = InstallmentStatus.Pending
                    };

                    await _installmentRepository.AddAsync(installment);
                }
            }

            await _unitOfWork.SaveChangesAsync();
            return payment.Id;
        }

        public async Task PayInstallmentAsync(PayInstallmentDto dto, Guid paidBy)
        {
            var installment = await _installmentRepository.GetByIdAsync(dto.InstallmentId)
                ?? throw new InvalidOperationException("Installment not found.");

            if (installment.Status == InstallmentStatus.Paid)
                throw new InvalidOperationException("Installment already paid.");

            installment.Status = InstallmentStatus.Paid;
            installment.PaidAt = dto.PaidAt;

            _installmentRepository.Update(installment);

            var payment = await _paymentRepository.GetByIdAsync(installment.PaymentId)
                ?? throw new InvalidOperationException("Payment not found.");

            var allInstallments = await _installmentRepository
                .GetByPaymentIdAsync(payment.Id);

            payment.Status = allInstallments.All(x => x.Status == InstallmentStatus.Paid)
                ? PaymentStatus.Paid
                : PaymentStatus.PartiallyPaid;

            _paymentRepository.Update(payment);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
