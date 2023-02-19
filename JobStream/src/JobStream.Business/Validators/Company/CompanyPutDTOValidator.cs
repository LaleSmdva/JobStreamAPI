using FluentValidation;
using JobStream.Business.DTOs.ArticleDTO;
using JobStream.Business.DTOs.CompanyDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Validators.Company;

public class CompanyPutDTOValidator : AbstractValidator<CompanyPutDTO>
{
	public CompanyPutDTOValidator()
	{

	}
}
