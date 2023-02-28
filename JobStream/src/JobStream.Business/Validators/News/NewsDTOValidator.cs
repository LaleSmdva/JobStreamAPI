using FluentValidation;
using JobStream.Business.DTOs.JobScheduleDTO;
using JobStream.Business.DTOs.NewsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Validators.News
{
    public class NewsDTOValidator : AbstractValidator<NewsDTO>
    {
        public NewsDTOValidator()
        {
            RuleFor(c => c.Title)
           .NotNull()
           .NotEmpty();

            RuleFor(c => c.Content)
            .NotNull()
            .NotEmpty();

            RuleFor(c => c.RubricForNewsId)
            .NotNull()
            .NotEmpty();
        }
    }
}
