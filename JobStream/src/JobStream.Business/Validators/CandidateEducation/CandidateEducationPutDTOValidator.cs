using FluentValidation;
using JobStream.Business.DTOs.ArticleDTO;
using JobStream.Business.DTOs.CandidateEducationDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Validators.CandidateEducation
{
    public class CandidateEducationPutDTOValidator : AbstractValidator<CandidateEducationPutDTO>
    {
        public CandidateEducationPutDTOValidator()
        {
            RuleFor(c => c.Major)
         .NotNull()
         .NotEmpty();

            RuleFor(c => c.Degree)
            .NotNull()
            .NotEmpty();

            RuleFor(c => c.Institution)
            .NotNull()
            .NotEmpty();
        }
    }
}
