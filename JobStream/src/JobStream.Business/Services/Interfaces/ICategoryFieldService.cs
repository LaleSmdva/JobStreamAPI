using JobStream.Business.DTOs.CategoryDTO;
using JobStream.Business.DTOs.CategoryFieldDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Services.Interfaces
{
    public interface ICategoryFieldService
    {
        Task<List<CategoryFieldDTO>> GetAll();
        Task CreateCategoryFieldAsync(CategoryFieldPostDTO entity);
        Task UpdateCategoryFieldNameAsync(int id, CategoryFieldPutDTO entity);
        Task DeleteCategoryFieldAsync(int id);
    }
}
