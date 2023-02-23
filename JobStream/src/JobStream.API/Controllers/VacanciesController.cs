using JobStream.Business.DTOs.CompanyDTO;
using JobStream.Business.DTOs.VacanciesDTO;
using JobStream.Business.Exceptions;
using JobStream.Business.Services.Implementations;
using JobStream.Business.Services.Interfaces;
using JobStream.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace JobStream.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	////  ApplyVacancy-int vacancyId,[frombody]CandidateResume(AppUser user var) elave et
	///then create new application
	public class VacanciesController : ControllerBase
	{
		private readonly IVacanciesService _vacanciesService;

		public VacanciesController(IVacanciesService vacanciesService)
		{
			_vacanciesService = vacanciesService;
		}

		//[HttpPost("ApplyVacancy")]

  //      public async Task<IActionResult> ApplyVacancy(VacanciesDTO vacancyId, [FromForm] CandidateResume candidateResume)
		//{
		//	await _vacanciesService.ApplyVacancy(vacancyId, candidateResume);
		//	return Ok();
		//}


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
				await _vacanciesService.CreateVacancyAsync(vacancy);
				return Ok("Successfully created");
			}
			catch (Exception)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError);
			}
		}

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVacancy(int id,[FromQuery] VacanciesPutDTO vacancy)
        {

            try
            {
                await _vacanciesService.UpdateVacancyAsync(id,vacancy);
                return Ok("Successfully updated");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

    }
}
