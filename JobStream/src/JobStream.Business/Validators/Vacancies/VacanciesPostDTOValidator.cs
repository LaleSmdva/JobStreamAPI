using FluentValidation;
using JobStream.Business.DTOs.CompanyDTO;
using JobStream.Business.DTOs.VacanciesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Validators.Vacancies
{
	public class VacanciesPostDTOValidator : AbstractValidator<VacanciesPostDTO>
	{
		public VacanciesPostDTOValidator()
		{
			RuleFor(v=>v.Name).NotNull().WithMessage("Name is required").NotEmpty().WithMessage("Name is required");
			RuleFor(v=>v.Requirements).NotNull().WithMessage("Requirements is required").NotEmpty().WithMessage("Requirements is required");
			RuleFor(v=>v.Description).NotNull().WithMessage("Description is required").NotEmpty().WithMessage("Description is required");
		}
	}
}
