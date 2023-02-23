using JobStream.Business.DTOs.JobScheduleDTO;
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
    public class JobSchedulesController : ControllerBase
    {
        private readonly IJobScheduleService _jobScheduleService;

        public JobSchedulesController(IJobScheduleService jobScheduleService)
        {
            _jobScheduleService = jobScheduleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllJobSchedulesAsync()
        {
            try
            {
                var jobSchedules = await _jobScheduleService.GetAllJobSchedulesAsync();
                return Ok(jobSchedules);
            }
            catch (Exception)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetJobScheduleByIdAsync(int id)
        {
            try
            {
                var article = await _jobScheduleService.GetJobScheduleByIdAsync(id);
                return Ok(article);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("GetAllVacancies")]
        public async Task<IActionResult> GetAllVacanciesByJobScheduleIdAsync(int id)
        {
            try
            {
                var vacancies =await _jobScheduleService.GetAllVacanciesByJobScheduleIdAsync(id);
                return Ok(vacancies);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
   

        [HttpPost]
        public async Task<IActionResult> CreateJobScheduleAsync(JobSchedulePostDTO entity)
        {
            try
            {
                await _jobScheduleService.CreateJobScheduleAsync(entity);
                return Ok("Article created");
            }
            catch (NullReferenceException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult>  UpdateJobScheduleAsync(int id, JobSchedulePutDTO jobSchedulePutDTO)
        {
            try
            {
                await _jobScheduleService.UpdateJobScheduleAsync(id, jobSchedulePutDTO);
                return Ok("Successfully updated");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>  DeleteJobScheduleAsync(int id)
        {
            try
            {
                await _jobScheduleService.DeleteJobScheduleAsync(id);
                return Ok("Article deleted");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
