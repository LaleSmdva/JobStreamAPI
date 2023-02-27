using JobStream.Business.DTOs.RubricForArticlesDTO;
using JobStream.Business.DTOs.RubricForNewsDTO;
using JobStream.Business.Services.Implementations;
using JobStream.Business.Services.Interfaces;
using JobStream.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobStream.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RubricForArticlesController : ControllerBase
    {
        private readonly IRubricForArticlesService _rubricForArticlesService;

        public RubricForArticlesController(IRubricForArticlesService rubricForArticlesService)
        {
            _rubricForArticlesService = rubricForArticlesService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rubricForArticles = await _rubricForArticlesService.GetAllAsync();
            return Ok(rubricForArticles);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] RubricForArticlesPostDTO entity)
        {
            await _rubricForArticlesService.CreateRubricForArticlesAsync(entity);
            return Ok("Rubric for article created");
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] RubricForArticlesPutDTO rubricForArticles)
        {
            await _rubricForArticlesService.UpdateRubricForArticlesAsync(id, rubricForArticles);
            return Ok("Successfully updated");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _rubricForArticlesService.DeleteRubricForArticlesAsync(id);
            return Ok("Rubric for article deleted");
        }
    }
}
