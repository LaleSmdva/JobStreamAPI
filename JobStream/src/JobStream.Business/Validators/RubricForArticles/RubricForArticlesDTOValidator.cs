﻿using FluentValidation;
using JobStream.Business.DTOs.NewsDTO;
using JobStream.Business.DTOs.RubricForArticlesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Validators.RubricForArticles
{
    public class RubricForArticlesDTOValidator : AbstractValidator<RubricForArticlesDTO>
    {
        public RubricForArticlesDTOValidator()
        {
            RuleFor(c => c.Name)
             .NotNull()
             .NotEmpty();
        }
    }
}
