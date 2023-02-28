using FluentValidation;
using JobStream.Business.DTOs.ApplicationsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Validators.Applications
{
    public class ApplicationsPutDTOValidator : AbstractValidator<ApplicationsPutDTO>
    {
        public ApplicationsPutDTOValidator()
        {

        }
    }
}
