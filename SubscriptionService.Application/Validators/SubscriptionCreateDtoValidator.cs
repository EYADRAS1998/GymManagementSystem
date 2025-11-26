using FluentValidation;
using SubscriptionService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionService.Application.Validators
{
    public class SubscriptionCreateDtoValidator : AbstractValidator<SubscriptionCreateDto>
    {
        public SubscriptionCreateDtoValidator()
        {
            RuleFor(x => x.MemberId)
                .NotEmpty().WithMessage("MemberId is required.");

            RuleFor(x => x.PlanId)
                .NotEmpty().WithMessage("PlanId is required.");

            RuleFor(x => x.StartDate)
                .LessThan(x => x.EndDate)
                .WithMessage("StartDate must be before EndDate.");
        }
    }
}
