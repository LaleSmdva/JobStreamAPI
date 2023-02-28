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

        [HttpGet]
        public IActionResult GetAllVacancies()
        {
            var companies = _vacanciesService.GetAll();
            return Ok(companies);
        }

        //[HttpPost("create")]
        //public async Task<IActionResult> AddCompany(VacanciesPostDTO vacancy)
        //{
        //    await _vacanciesService.CreateVacancyAsync(vacancy);
        //    return Ok("Successfully created");
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateVacancy(int id, [FromQuery] VacanciesPutDTO vacancy)
        //{
        //    await _vacanciesService.UpdateVacancyAsync(id, vacancy);
        //    return Ok("Successfully updated");
        //}

        [HttpPost("vuy")]
        public async Task<IActionResult> Cleanup()
        {
            await _vacanciesService.VacancyCleanUp();
            return Ok("Hangfire successed");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SearchVacancies([FromQuery]string? keyword, [FromQuery] string? location, [FromQuery] List<int>? categoryId, [FromQuery] string? companyName)
        {
            List<VacanciesDTO> list=await _vacanciesService.SearchVacancies(keyword, location, categoryId, companyName);
            return Ok(list);
        }

    }
}
