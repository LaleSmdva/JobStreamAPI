using FluentValidation;
using JobStream.Business.DTOs.ArticleDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Validators.Article;

public class ArticlePostDTOValidator : AbstractValidator<ArticlePostDTO>
{
	public ArticlePostDTOValidator()
	{

	}
}
