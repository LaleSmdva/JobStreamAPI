using FluentValidation;
using JobStream.Business.DTOs.CategoryDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Validators.Category
{
    public class CategoryPutDTOValidator : AbstractValidator<CategoriesPutDTO>
    {
        public CategoryPutDTOValidator()
        {
            RuleFor(c => c.Name).NotNull().NotEmpty().WithMessage("Enter category name.");
        }
    }
}
