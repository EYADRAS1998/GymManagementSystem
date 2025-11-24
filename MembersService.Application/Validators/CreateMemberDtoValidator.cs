using FluentValidation;
using MembersService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembersService.Application.Validators
{
    public class CreateMemberDtoValidator : AbstractValidator<CreateMemberDto>
    {
        public CreateMemberDtoValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required")
                .MaximumLength(200);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required")
                .MaximumLength(20);

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Invalid email address")
                .MaximumLength(100)
                .When(x => !string.IsNullOrEmpty(x.Email));

            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage("Gender is required")
                .Must(g => g == "Male" || g == "Female")
                .WithMessage("Gender must be 'Male' or 'Female'");

            RuleFor(x => x.BirthDate)
                .LessThan(DateTime.UtcNow).WithMessage("BirthDate must be in the past");

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("CreatedBy is required");
        }
    }
}
