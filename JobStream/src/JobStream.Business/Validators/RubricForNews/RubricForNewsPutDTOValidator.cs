using FluentValidation;
using JobStream.Business.DTOs.NewsDTO;
using JobStream.Business.DTOs.RubricForNewsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Validators.RubricForNews
{
    public class RubricForNewsPutDTOValidator : AbstractValidator<RubricForNewsPutDTO>
    {
        public RubricForNewsPutDTOValidator()
        {
            RuleFor(c => c.Name)
            .NotNull()
            .NotEmpty();
        }
    }
}
