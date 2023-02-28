using FluentValidation;
using JobStream.Business.DTOs.CategoryDTO;
using JobStream.Business.DTOs.CategoryFieldDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Validators.CategoryField
{
    public class CategoryFieldPutDTOValidator : AbstractValidator<CategoryFieldPutDTO>
    {
        public CategoryFieldPutDTOValidator()
        {
            RuleFor(c => c.Name).NotNull().NotEmpty().WithMessage("Enter category name.");
        }
    }
}
