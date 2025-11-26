using FluentValidation;
using SubscriptionService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionService.Application.Validators
{
    public class SubscriptionUpdateDtoValidator : AbstractValidator<SubscriptionUpdateDto>
    {
        public SubscriptionUpdateDtoValidator()
        {
            RuleFor(x => x.PlanId)
                .NotEmpty().WithMessage("PlanId is required.");

            RuleFor(x => x.StartDate)
                .LessThan(x => x.EndDate)
                .WithMessage("StartDate must be before EndDate.");

            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate)
                .WithMessage("EndDate must be after StartDate.");
        }
    }
}
