using JobStream.Business.DTOs.NewsDTO;
using JobStream.Business.DTOs.RubricForNewsDTO;
using JobStream.Business.Services.Implementations;
using JobStream.Business.Services.Interfaces;
using JobStream.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobStream.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RubricForNewsController : ControllerBase
{
    private readonly IRubricForNewsService _rubricForNewsService;

    public RubricForNewsController(IRubricForNewsService rubricForNewsService)
    {
        _rubricForNewsService = rubricForNewsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var rubricForNews = await _rubricForNewsService.GetAllAsync();
        return Ok(rubricForNews);
    }

    [HttpPost]
    public async Task<IActionResult> Create(RubricForNewsPostDTO entity)
    {
        await _rubricForNewsService.CreateRubricForNewsAsync(entity);
        return Ok("Rubric for news created");
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id,RubricForNewsPutDTO rubricForNews)    
    {
        await _rubricForNewsService.UpdateRubricForNewsAsync(id, rubricForNews);
        return Ok("Successfully updated");
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _rubricForNewsService.DeleteRubricForNewsAsync(id);
        return Ok("Rubric for news deleted");
    }

}
