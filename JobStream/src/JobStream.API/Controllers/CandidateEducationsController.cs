using JobStream.Business.DTOs.CandidateEducationDTO;
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


        [HttpGet]
        public async Task<IActionResult> GetAllCandidatesEducations()
        {
            var list = await _candidateEducation.GetAllCandidatesEducationAsync();
            return Ok(list);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetCandidateEducationByResumeId(int id)
        {
            var candidateEducation = await _candidateEducation.GetCandidateEducationByResumeIdAsync(id);
            return Ok(candidateEducation);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CandidateEducationPostDTO entity)
        {
            await _candidateEducation.CreateCandidateEducationeAsync(entity);
            return Ok("Candidate education created");

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CandidateEducationPutDTO education)
        {
            await _candidateEducation.UpdateCandidateEducationAsync(id, education);
            return Ok("Successfully updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _candidateEducation.DeleteCandidateEducation(id);
            return Ok("Candidate education deleted");
        }
    }
}
