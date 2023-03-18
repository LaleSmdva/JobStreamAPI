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


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CandidateEducationPutDTO education)
        {
            await _candidateEducation.UpdateCandidateEducationAsync(id, education);
            return Ok("Successfully updated");
        }

        [HttpDelete("{candidateId}")]
        public async Task<IActionResult>  DeleteCandidateEducationInfoAsync(int candidateId, List<int> educationIds)
        {
            await _candidateEducation.DeleteCandidateEducationInfoAsync(candidateId,educationIds);
            return Ok("Candidate education deleted");
        }

    }
}
