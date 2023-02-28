using FluentValidation;
using JobStream.Business.DTOs.JobScheduleDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Validators.JobSchedule
{
    public class JobSchedulePutDTOValidator : AbstractValidator<JobSchedulePutDTO>
    {
        public JobSchedulePutDTOValidator()
        {
            RuleFor(s => s.Schedule).NotNull().WithMessage("Job schedule can'be null")
         .NotEmpty().WithMessage("Job schedule can't be empty")
         .MaximumLength(100).WithMessage("Max length is 100");
        }
    }
}
