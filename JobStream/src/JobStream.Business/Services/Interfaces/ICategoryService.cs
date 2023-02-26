using JobStream.Business.DTOs.CategoryDTO;
using JobStream.Business.DTOs.CompanyDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Services.Interfaces;

public interface ICategoryService
{
    Task<List<CategoriesDTO>> GetAllCategories();
    Task CreateCategoryAsync(CategoriesPostDTO entity);
    Task UpdateCategoryNameAsync(int id,CategoriesPutDTO entity);
    Task DeleteCategoryAsync(int id);
}
