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

        }
    }
}
