using FluentValidation;
using JobStream.Business.DTOs.ArticleDTO;
using JobStream.Business.DTOs.CompanyDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Validators.Company;

public class CompanyPostDTOValidator : AbstractValidator<CompanyPostDTO>
{
	public CompanyPostDTOValidator()
	{
        RuleFor(c => c.Name).NotNull().WithMessage("Enter name").NotEmpty().WithMessage("Enter name")
            .Length(3, 100).WithMessage("Comoany  name can have at least 3 and max of 100 characters.");
        RuleFor(c => c.AboutCompany).NotNull().WithMessage("Enter about your company.")
            .NotEmpty().WithMessage("Enter about your company.")
            .MaximumLength(500).WithMessage("Max characters:500");
        RuleFor(c => c.Email).NotNull().WithMessage("Enter email.")
         .NotEmpty().WithMessage("Enter email.")
         .EmailAddress().WithMessage("Enter valid email address");
      
    }
}
