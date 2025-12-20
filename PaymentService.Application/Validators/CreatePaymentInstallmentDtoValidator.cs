using FluentValidation;
using PaymentService.Application.DTOs.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.Validators
{
    public class CreatePaymentInstallmentDtoValidator
           : AbstractValidator<CreatePaymentInstallmentDto>
    {
        public CreatePaymentInstallmentDtoValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("Installment amount must be greater than zero.");

            RuleFor(x => x.DueDate)
                .GreaterThan(DateTime.UtcNow)
                .WithMessage("Due date must be in the future.");
        }
    }
}
