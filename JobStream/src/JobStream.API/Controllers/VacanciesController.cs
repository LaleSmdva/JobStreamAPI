using JobStream.Business.DTOs.CompanyDTO;
using JobStream.Business.DTOs.VacanciesDTO;
using JobStream.Business.Exceptions;
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

		[HttpPost("create")]
		public async Task<IActionResult> AddCompany(VacanciesPostDTO vacancy)
		{

			try
			{
				//var existingCompany = await _vacanciesService.GetByIdAsync(company.Id);
				//var companies = _vacanciesService.GetAll();
				//if (companies.Any(c => c.Name == company.Name && c.Email == company.Email))
				//{
				//	throw new BadRequestException("A company with the same name and email already exists");
				//}

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
