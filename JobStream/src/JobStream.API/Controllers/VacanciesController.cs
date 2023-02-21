using JobStream.Business.DTOs.CompanyDTO;
using JobStream.Business.DTOs.VacanciesDTO;
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
	public class VacanciesController : ControllerBase
	{
		private readonly IVacanciesService _vacanciesService;

		public VacanciesController(IVacanciesService vacanciesService)
		{
			_vacanciesService = vacanciesService;
		}

        [HttpGet("")]
        public IActionResult GetAllVacancies()
		{
			try
			{
                var companies = _vacanciesService.GetAll();
                return Ok(companies);
            }
            catch (NotFoundException)
            {
                return NotFound("Not Found");
            }
            //catch (Exception)
            //{
            //	throw new InvalidOperationException("The requested resource could not be found.");	

            //}
        }

        [HttpPost("create")]
		public async Task<IActionResult> AddCompany(VacanciesPostDTO vacancy)
		{

			try
			{
				await _vacanciesService.CreateAsync(vacancy);
				return Ok("Successfully created");
			}
			catch (Exception)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError);
			}
		}

	}
}
