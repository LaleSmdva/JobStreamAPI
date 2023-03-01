﻿using JobStream.Business.DTOs.ApplyVacancyDTO;
using JobStream.Business.DTOs.CandidateResumeDTO;
using JobStream.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

      
 
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCandidateResumeByUserId(string userId)
        {
            var resume = await _candidateResumeService.GetCandidateResumeByUserId(userId);
            return Ok(resume);
        }

        [HttpGet("AcceptedVacancies")]
        public async Task<IActionResult> GetAcceptedVacancies()
        {
            var list = await _candidateResumeService.GetAcceptedVacancies();
            return Ok(list);
        }

        [HttpGet("RejectedVacancies")]
        public async Task<IActionResult> GetRejectedVacancies()
        {
            var list = await _candidateResumeService.GetRejectedVacancies();
            return Ok(list);
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] CandidateResumePutDTO resume)
        {
            await _candidateResumeService.UpdateCandidateResumeAsync(id, resume);
            return Ok("Successfully updated");
        }

     

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _candidateResumeService.DeleteCandidateResume(id);
            return Ok("Candidate resume deleted");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ApplyVacancy(int candidateId, int companyId, int vacancyId, [FromForm] ApplyVacancyDTO applyVacancyDTO)
        {
            await _candidateResumeService.ApplyVacancy(candidateId, companyId, vacancyId, applyVacancyDTO);
            return Ok("Your application has been received");
        }
        [HttpGet("AppliedVacanciesStatus")]
        public async Task<IActionResult> ViewAppliedJobs(int candidateId)
        {
            var appliedJobs = await _candidateResumeService.ViewStatusOfAppliedJobs(candidateId);
            return Ok(appliedJobs);
        }

    }
}
