using JobStream.Business.DTOs.ArticleDTO;
using JobStream.Business.DTOs.CategoryDTO;
using JobStream.Business.DTOs.CompanyDTO;
using JobStream.Business.Exceptions;
using JobStream.Business.Services.Implementations;
using JobStream.Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace JobStream.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCompanies()
        {
            var companies = await _categoryService.GetAllCategories();
            return Ok(companies);
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddCompany(CategoriesPostDTO category)
        {
            await _categoryService.CreateCategoryAsync(category);
            return Ok("Successfully created");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategoriesPutDTO category)
        {
            await _categoryService.UpdateCategoryNameAsync(id, category);
            return Ok("Successfully updated");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return Ok("Category deleted");
        }
    }
}
