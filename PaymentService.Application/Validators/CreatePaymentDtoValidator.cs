using FluentValidation;
using PaymentService.Application.DTOs.Payments;
using PaymentService.Domian.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.Validators
{
    public class CreatePaymentDtoValidator
           : AbstractValidator<CreatePaymentDto>
    {
        public CreatePaymentDtoValidator()
        {
            RuleFor(x => x.SubscriptionId)
                .NotEmpty();

            RuleFor(x => x.MemberId)
                .NotEmpty();

            RuleFor(x => x.TotalAmount)
                .GreaterThan(0);

            RuleFor(x => x.Currency)
                .NotEmpty()
                .MaximumLength(3);

            RuleFor(x => x.PaymentType)
                .IsInEnum();

            // Cash payment rules
            When(x => x.PaymentType == PaymentType.Cash, () =>
            {
                RuleFor(x => x.Installments)
                    .Must(x => x == null || !x.Any())
                    .WithMessage("Cash payment must not have installments.");
            });

            // Installments payment rules
            When(x => x.PaymentType == PaymentType.Installments, () =>
            {
                RuleFor(x => x.Installments)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage("Installments are required for installment payments.");

                RuleForEach(x => x.Installments!)
                    .SetValidator(new CreatePaymentInstallmentDtoValidator());

                RuleFor(x => x)
                    .Must(HaveValidInstallmentsTotal)
                    .WithMessage("Total installments amount must equal total payment amount.");
            });
        }

        private bool HaveValidInstallmentsTotal(CreatePaymentDto dto)
        {
            if (dto.Installments == null)
                return false;

            var installmentsTotal = dto.Installments.Sum(i => i.Amount);
            return installmentsTotal == dto.TotalAmount;
        }
    }
}
