using JobStream.Business.DTOs.ArticleDTO;
using JobStream.Business.DTOs.NewsDTO;
using JobStream.Business.Services.Implementations;
using JobStream.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace JobStream.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NewsController : ControllerBase
	{
		private readonly INewsService _newsService;
        private readonly IRubricForNewsService _rubricForNewsService;

        public NewsController(INewsService newsService, IRubricForNewsService rubricForNewsService)
        {
            _newsService = newsService;
            _rubricForNewsService = rubricForNewsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNews()
        {
            var news = await _newsService.GetAll();
            return Ok(news);
        }


        [HttpGet("search/{title}")]
        public IActionResult GetNewsByTitle(string title)
        {
            var news = _newsService.GetNewsByTitle(title);
            return Ok(news);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetNewsById(int id)
        {
            var news = await _newsService.GetNewsByIdAsync(id);
            return Ok(news);
        }


        //[HttpGet("{rubricId}")]
        //public async Task<IActionResult> GetNewsByRubricId(int rubricId)
        //{
        //    var news = await _newsService.GetNewsByRubricId(rubricId);
        //    return Ok(news);
        //}


        [HttpPost]
        //[Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> CreateAsync([FromForm]NewsPostDTO entity)
        {
            await _newsService.CreateNewsAsync(entity);
            return Ok("News created");
        }


        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> Update(int id, [FromForm] NewsPutDTO news)
        {
            await _newsService.UpdateNewsAsync(id, news);
            return Ok("Successfully updated");
        }


        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> DeleteNews(int id)
        {
            await _newsService.DeleteNewsAsync(id);
            return Ok("News deleted");
        }
    }
}
