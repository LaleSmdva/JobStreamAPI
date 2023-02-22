using JobStream.Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
