using FluentValidation;
using JobStream.Business.DTOs.JobScheduleDTO;
using JobStream.Business.DTOs.JobTypeDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Validators.JobType
{
    public class JobTypePutDTOValidator : AbstractValidator<JobTypePutDTO>
    {
        public JobTypePutDTOValidator()
        {
            RuleFor(s => s.Name).NotNull().WithMessage("Job type can'be null")
.NotEmpty().WithMessage("Job type can't be empty")
.MaximumLength(100).WithMessage("Max length is 100");
        }
    }
}
