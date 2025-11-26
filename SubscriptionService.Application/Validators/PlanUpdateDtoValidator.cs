using FluentValidation;
using SubscriptionService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionService.Application.Validators
{
    public class PlanUpdateDtoValidator : AbstractValidator<PlanUpdateDto>
    {
        public PlanUpdateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Plan name is required.")
                .MaximumLength(100).WithMessage("Plan name must not exceed 100 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");
        }
    }
}
