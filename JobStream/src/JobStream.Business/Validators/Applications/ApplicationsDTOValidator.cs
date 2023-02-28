using FluentValidation;
using JobStream.Business.DTOs.ApplicationsDTO;
using JobStream.Business.DTOs.ArticleDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Validators.Applications
{
    public class ApplicationsDTOValidator : AbstractValidator<ApplicationsDTO>
    {
        public ApplicationsDTOValidator()
        {

        }
    }
}
