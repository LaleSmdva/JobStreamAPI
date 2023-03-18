using JobStream.Business.DTOs.ApplyVacancyDTO;
using JobStream.Business.DTOs.CandidateResumeDTO;
using JobStream.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace JobStream.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateResumesController : ControllerBase
    {
        private readonly ICandidateResumeService _candidateResumeService;

        public CandidateResumesController(ICandidateResumeService candidateResumeService)
        {
            _candidateResumeService = candidateResumeService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCandidateResumeByUserId(string userId)
        {
            var resume = await _candidateResumeService.GetCandidateResumeByUserId(userId);
            return Ok(resume);
        }

        [HttpGet("{candidateId}/AcceptedVacancies")]
        //[Authorize(Roles = "Admin,Moderator,Candidate")]
        public async Task<IActionResult> GetAcceptedVacancies(int candidateId)
        {
            var list = await _candidateResumeService.GetAcceptedVacancies(candidateId);
            return Ok(list);
        }

        [HttpGet("{candidateId}/RejectedVacancies")]
        //[Authorize(Roles = "Admin,Moderator,Candidate")]
        public async Task<IActionResult> GetRejectedVacancies(int candidateId)
        {
            var list = await _candidateResumeService.GetRejectedVacancies(candidateId);
            return Ok(list);
        }
        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin,Moderator,Candidate")]
        public async Task<IActionResult> Update(int id, [FromForm] CandidateResumePutDTO resume)
        {
            await _candidateResumeService.UpdateCandidateResumeAsync(id, resume);
            return Ok("Successfully updated");
        }

     

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin,Moderator,Candidate")]
        public async Task<IActionResult> Delete(int id)
        {
            await _candidateResumeService.DeleteCandidateResume(id);
            return Ok("Candidate resume deleted");
        }

        [HttpPost("[action]/{candidateId}/{companyId}/{vacancyId}")]
        //[Authorize(Roles = "Admin,Moderator,Candidate")]
        public async Task<IActionResult> ApplyVacancy(int candidateId, int companyId, int vacancyId, [FromForm] ApplyVacancyDTO applyVacancyDTO)
        {
            await _candidateResumeService.ApplyVacancy(candidateId, companyId, vacancyId, applyVacancyDTO);
            return Ok("Your application has been received");
        }
        [HttpGet("{candidateId}/[action]")]
        //[Authorize(Roles = "Admin,Moderator,Candidate")]
        public async Task<IActionResult> ViewAppliedJobs(int candidateId)
        {
            var appliedJobs = await _candidateResumeService.ViewStatusOfAppliedJobs(candidateId);
            return Ok(appliedJobs);
        }

    }
}
