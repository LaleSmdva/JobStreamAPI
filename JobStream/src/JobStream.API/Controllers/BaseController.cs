using JobStream.DataAccess.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobStream.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BaseController : ControllerBase
	{
		private readonly AppDbContext _context;

		public BaseController(AppDbContext context)
		{
			_context = context;
		}
	}
}
