using JobStream.Business.DTOs.CategoryFieldDTO;
using JobStream.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobStream.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryFieldsController : ControllerBase
    {
        private readonly ICategoryFieldService _categoryFieldService;

        public CategoryFieldsController(ICategoryFieldService categoryFieldService)
        {
            _categoryFieldService = categoryFieldService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categoryFields = await _categoryFieldService.GetAll();
            return Ok(categoryFields);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryFieldPostDTO entity)
        {
            await _categoryFieldService.CreateCategoryFieldAsync(entity);
            return Ok("Successfully created");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategoryFieldPutDTO entity)
        {
            await _categoryFieldService.UpdateCategoryFieldNameAsync(id, entity);
            return Ok("Successfully updated");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryFieldService.DeleteCategoryFieldAsync(id);
            return Ok("Category field deleted");
        }
    }
}
