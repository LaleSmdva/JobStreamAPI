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
    public class NewsDTOPostValidator : AbstractValidator<NewsPostDTO>
    {
        public NewsDTOPostValidator()
        {
            RuleFor(c => c.Title)
     .NotNull()
     .NotEmpty()
     .MaximumLength(200)
     .WithMessage("Max length for title is 200 symbols");

            RuleFor(c => c.Content)
            .NotNull()
            .NotEmpty();

            RuleFor(c => c.RubricForNewsId)
            .NotNull()
            .NotEmpty();
        }
    }
}
