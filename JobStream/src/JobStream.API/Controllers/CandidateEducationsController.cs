using JobStream.Business.DTOs.JobTypeDTO;
using JobStream.Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobStream.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateEducationsController : ControllerBase
    {
        private readonly ICandidateEducationService _candidateEducation;

        public CandidateEducationsController(ICandidateEducationService candidateEducation)
        {
            _candidateEducation = candidateEducation;
        }
        

        //[HttpGet]
        //public async Task<IActionResult> GetAllJobTypes()
        //{
        //    var jobTypes = await _candidateEducation.();
        //    return Ok(jobTypes);
        //}


        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetJobTypeById(int id)
        //{

        //    var jobType = await _candidateEducation.GetJobTypeByIdAsync(id);
        //    return Ok(jobType);
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCandidateEducationByResumeIdAsync(int id)
        {
            var candidateEducation = await _candidateEducation.GetCandidateEducationByResumeIdAsync(id);
            return Ok(candidateEducation);
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateAsync(JobTypePostDTO entity)
        //{
        //    await _candidateEducation.CreateJobTypeAsync(entity);
        //    return Ok("Candidate education created");

        //}
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(int id, JobTypePutDTO jobType)
        //{
        //    await _candidateEducation.UpdateJobTypeAsync(id, jobType);
        //    return Ok("Successfully updated");
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    await _candidateEducation.DeleteJobTypeAsync(id);
        //    return Ok("Candidate education deleted");
        //}
    }
}
