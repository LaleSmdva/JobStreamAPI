using FluentValidation;
using JobStream.Business.DTOs.AboutUsDTO;
using JobStream.Business.DTOs.ArticleDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Validators.AboutUs
{
    public class AboutUsPutDTOValidator : AbstractValidator<AboutUsPutDTO>
    {
        public AboutUsPutDTOValidator()
        {
            RuleFor(c => c.Email)
         .NotNull()
         .NotEmpty()
         .EmailAddress().WithMessage("Enter valid email address");

            RuleFor(c => c.Telephone)
            .NotNull()
            .NotEmpty()
            .Matches(@"^\+?\d{1,3}?[-.\s]?\d{3,}[-.\s]?\d{4}$")
            .WithMessage("Invalid telephone number format.");
        }
    }
}
