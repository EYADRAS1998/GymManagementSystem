using FluentValidation;
using MembersService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembersService.Application.Validators
{
    public class UpdateMemberProgressDtoValidator : AbstractValidator<UpdateMemberProgressDto>
    {
        public UpdateMemberProgressDtoValidator()
        {
            RuleFor(x => x.Weight)
                .GreaterThan(0).WithMessage("Weight must be greater than 0");

            RuleFor(x => x.MeasurementsJson)
                .NotEmpty().WithMessage("MeasurementsJson is required");

            RuleFor(x => x.RecordedBy)
                .NotEmpty().WithMessage("RecordedBy is required");
        }
    }
}
