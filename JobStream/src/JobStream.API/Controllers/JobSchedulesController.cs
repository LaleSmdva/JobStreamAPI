using JobStream.Business.DTOs.JobScheduleDTO;
using JobStream.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
            var jobSchedules = await _jobScheduleService.GetAllJobSchedulesAsync();
            return Ok(jobSchedules);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobScheduleByIdAsync(int id)
        {
            var jobSchedule = await _jobScheduleService.GetJobScheduleByIdAsync(id);
            return Ok(jobSchedule);
        }
        [HttpGet("{id}/Vacancies")]
        public async Task<IActionResult> GetAllVacanciesByJobScheduleIdAsync(int id)
        {
            var vacancies = await _jobScheduleService.GetAllVacanciesByJobScheduleIdAsync(id);
            return Ok(vacancies);
        }
        [HttpPost]
        public async Task<IActionResult> CreateJobScheduleAsync(JobSchedulePostDTO entity)
        {
            await _jobScheduleService.CreateJobScheduleAsync(entity);
            return Ok("Job schedule created");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJobScheduleAsync(int id, JobSchedulePutDTO jobSchedulePutDTO)
        {
            await _jobScheduleService.UpdateJobScheduleAsync(id, jobSchedulePutDTO);
            return Ok("Successfully updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobScheduleAsync(int id)
        {
            await _jobScheduleService.DeleteJobScheduleAsync(id);
            return Ok("Job schedule deleted");
        }
    }
}
