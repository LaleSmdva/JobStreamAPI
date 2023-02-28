using FluentValidation;
using JobStream.Business.DTOs.CandidateResumeDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Validators.CandidateResume
{
    public class CandidateResumePutDTOValidator : AbstractValidator<CandidateResumePutDTO>
    {
        public CandidateResumePutDTOValidator()
        {
            RuleFor(c => c.CV).NotNull().WithMessage("Choose CV").NotEmpty().WithMessage("Choose CV");
            RuleFor(c => c.ProfilePhoto).NotNull().WithMessage("Choose profile picture").NotEmpty().WithMessage("Choose profile picture");

            RuleFor(c => c.Fullname).NotNull().WithMessage("Enter your fullname.")
                .NotEmpty().WithMessage("Enter your fullname.")
                .Length(3, 150).WithMessage("Character range : 3-150.");

            RuleFor(c => c.Email).NotNull().WithMessage("Enter email.")
             .NotEmpty().WithMessage("Enter email.")
             .EmailAddress().WithMessage("Enter valid email address");

            RuleFor(c => c.AboutMe).NotNull().WithMessage("Enter about yourself.")
           .NotEmpty().WithMessage("Enter about yourself.")
           .MaximumLength(500).WithMessage("Max characters:500");
        }
    }
}
