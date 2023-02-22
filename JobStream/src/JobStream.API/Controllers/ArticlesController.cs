using JobStream.Business.Services.Interfaces;
using JobStream.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
