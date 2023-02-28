using FluentValidation;
using JobStream.Business.DTOs.NewsDTO;
using JobStream.Business.DTOs.RubricForArticlesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Validators.RubricForArticles
{
    public class RubricForArticlesPutDTOValidator : AbstractValidator<RubricForArticlesPutDTO>
    {
        public RubricForArticlesPutDTOValidator()
        {
            RuleFor(c => c.Name)
            .NotNull()
            .NotEmpty();
        }
    }
}
