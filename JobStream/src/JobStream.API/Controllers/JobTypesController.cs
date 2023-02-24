using JobStream.Business.DTOs.ArticleDTO;
using JobStream.Business.DTOs.JobTypeDTO;
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
    public class JobTypesController : ControllerBase
    {
        private readonly IJobTypeService _jobTypeService;

        public JobTypesController(IJobTypeService jobTypeService)
        {
            _jobTypeService = jobTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllJobTypes()
        {
            var jobTypes = await _jobTypeService.GetAllJobTypesAsync();
            return Ok(jobTypes);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobTypeById(int id)
        {

            var jobType = await _jobTypeService.GetJobTypeByIdAsync(id);
            return Ok(jobType);
        }

        [HttpGet("GetVacanciesByJobTypeId/{id}")]
        public async Task<IActionResult> GetVacanciesByJobTypeId(int id)
        {

            var jobTypes = await _jobTypeService.GetVacanciesByJobTypeId(id);
            return Ok(jobTypes);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(JobTypePostDTO entity)
        {
            await _jobTypeService.CreateJobTypeAsync(entity);
            return Ok("Job type created");

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, JobTypePutDTO jobType)
        {
            await _jobTypeService.UpdateJobTypeAsync(id, jobType);
            return Ok("Successfully updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _jobTypeService.DeleteJobTypeAsync(id);
            return Ok("Job type deleted");
        }
    }
}
