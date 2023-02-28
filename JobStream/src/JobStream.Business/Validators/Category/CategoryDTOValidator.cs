using FluentValidation;
using JobStream.Business.DTOs.CandidateEducationDTO;
using JobStream.Business.DTOs.CategoryDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Validators.Category
{
    public class CategoryDTOValidator : AbstractValidator<CategoriesDTO>
    {
        public CategoryDTOValidator()
        {
            RuleFor(c => c.Name).NotNull().NotEmpty().WithMessage("Enter category name.");
        }
    }
}
