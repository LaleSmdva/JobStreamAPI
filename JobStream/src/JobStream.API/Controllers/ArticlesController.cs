using JobStream.Business.DTOs.ArticleDTO;
using JobStream.Business.Services.Interfaces;
using JobStream.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Net;

namespace JobStream.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly IRubricForArticlesService _rubricForArticlesService;

        public ArticlesController(IArticleService articleService, IRubricForArticlesService rubricForArticlesService)
        {
            _articleService = articleService;
            _rubricForArticlesService = rubricForArticlesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllArticles()
        {
            try
            {
                var articles = await _articleService.GetAllAsync();
                return Ok(articles);
            }
            catch (Exception)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("{title}")]
        public IActionResult GetArticleByTitle(string title, Expression<Func<Article, bool>> expression)
        {
            try
            {
                var articles = _articleService.GetByArticleTitle(title, expression);
                return Ok(articles);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticleById(int id)
        {
            try
            {
                var article = await _articleService.GetArticleByIdAsync(id);
                return Ok(article);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(ArticlePostDTO entity)
        {
            try
            {
                await _articleService.CreateArticleAsync(entity);
                return Ok("Article created");
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ArticlePutDTO article)
        {
            try
            {
                await _articleService.UpdateArticleAsync(id, article);
                return Ok("Successfully updated");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            try
            {
                await _articleService.DeleteArticleAsync(id);
                return Ok("Article deleted");
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
