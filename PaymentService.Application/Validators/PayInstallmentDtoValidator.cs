using FluentValidation;
using PaymentService.Application.DTOs.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.Validators
{
    public class PayInstallmentDtoValidator
            : AbstractValidator<PayInstallmentDto>
    {
        public PayInstallmentDtoValidator()
        {
            RuleFor(x => x.InstallmentId)
                .NotEmpty();

            RuleFor(x => x.PaidAt)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Paid date cannot be in the future.");
        }
    }
}
