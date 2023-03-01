﻿using JobStream.Business.DTOs.ArticleDTO;
using JobStream.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobStream.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticlesController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllArticles()
        {
            var articles = await _articleService.GetAll();
            return Ok(articles);
        }


        [HttpGet("GetArticleByTitle/{title}")]
        public IActionResult GetArticleByTitle(string title)
        {
            var articles = _articleService.GetArticlesByTitle(title);
            return Ok(articles);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticleById(int id)
        {
            var article = await _articleService.GetArticleByIdAsync(id);
            return Ok(article);
        }


        [HttpGet("articlesByRubricId{id}")]
        public async Task<IActionResult> GetArticlesByRubricId(int id)
        {
            var article = await _articleService.GetArticlesByRubricId(id);
            return Ok(article);
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] ArticlePostDTO entity)
        {
            await _articleService.CreateArticleAsync(entity);
            return Ok("Article created");
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] ArticlePutDTO article)
        {
            await _articleService.UpdateArticleAsync(id, article);
            return Ok("Successfully updated");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            await _articleService.DeleteArticleAsync(id);
            return Ok("Article deleted");
        }

    }
}
