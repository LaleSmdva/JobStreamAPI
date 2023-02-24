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
    public class AboutUsDTOValidator : AbstractValidator<AboutUsDTO>
    {
        public AboutUsDTOValidator()
        {
            RuleFor(a=>a.Id).GreaterThan(0).WithMessage("Id must be greater than 0");
            RuleFor(c => c.Email)
             .NotNull().WithMessage("Email address can not be null")
             .NotEmpty().WithMessage("Email address can not be empty")
             .EmailAddress().WithMessage("Enter valid email address");

            RuleFor(c => c.Telephone)
            .NotNull().WithMessage("Email address can not be null")
            .NotEmpty().WithMessage("Email address can not be empty")
            .Matches(@"^\+?\d{1,3}?[-.\s]?\d{3,4}[-.\s]?\d{4}$")
            .WithMessage("Invalid telephone number format.");
        }
    }
}
