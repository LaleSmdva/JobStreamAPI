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
    List<CategoriesDTO> GetAll();
    Task CreateAsync(CategoriesPostDTO entity);
}
