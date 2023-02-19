using FluentValidation;
using JobStream.Business.DTOs.ArticleDTO;
using JobStream.Business.DTOs.VacanciesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Validators.Article;

public class ArticleDTOValidator : AbstractValidator<ArticleDTO>
{
	public ArticleDTOValidator()
	{

	}
}
