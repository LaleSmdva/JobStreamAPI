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
        RuleFor(c => c.Title)
          .NotNull()
          .NotEmpty();

        RuleFor(c => c.Description)
        .NotNull()
        .NotEmpty();

        RuleFor(c => c.RubricForArticlesId)
        .NotNull()
        .NotEmpty();
       

        RuleFor(c => c.Image).NotEmpty().WithMessage("Please provide a file.");
    }
}
