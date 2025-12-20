using PaymentService.Application.DTOs.Payments;
using PaymentService.Application.Services.Abstractions;
using PaymentService.Domian.Entities;
using PaymentService.Domian.Enums;
using PaymentService.Domian.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                SubscriptionId = dto.SubscriptionId,
                MemberId = dto.MemberId,
                TotalAmount = dto.TotalAmount,
                Currency = dto.Currency,
                PaymentType = dto.PaymentType,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = createdBy
            };

            if (dto.PaymentType == PaymentType.Cash)
            {
                payment.Status = PaymentStatus.Paid;
            }
            else
            {
                payment.Status = PaymentStatus.Pending;
            }

            await _paymentRepository.AddAsync(payment);

            if (dto.PaymentType == PaymentType.Installments)
            {
                foreach (var installmentDto in dto.Installments!)
                {
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

            if (allInstallments.All(x => x.Status == InstallmentStatus.Paid))
            {
                payment.Status = PaymentStatus.Paid;
            }
            else
            {
                payment.Status = PaymentStatus.PartiallyPaid;
            }

            _paymentRepository.Update(payment);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
