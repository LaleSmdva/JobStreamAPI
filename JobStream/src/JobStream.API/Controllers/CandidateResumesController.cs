﻿using JobStream.Business.DTOs.ApplyVacancyDTO;
using JobStream.Business.DTOs.CandidateEducationDTO;
using JobStream.Business.DTOs.CandidateResumeDTO;
using JobStream.Business.DTOs.VacanciesDTO;
using JobStream.Business.Exceptions;
using JobStream.Business.Services.Interfaces;
using JobStream.Core.Entities;
using Microsoft.AspNetCore.Http;
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

        [HttpGet]
        public async Task<IActionResult> GetAllCandidatesEducations()
        {
            var list = await _candidateResumeService.GetAllCandidatesResumesAsync();
            return Ok(list);
        }

        [HttpGet("details")]
        public async Task<IActionResult> GetCandidateResumeDetails(int resumeId)
        {
            var candidate = await _candidateResumeService.CandidateResumeDetails(resumeId);
            return Ok(candidate);
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetCandidateResumeByUserId(string resumeId)
        {
            var resume = await _candidateResumeService.GetCandidateResumeByUserId(resumeId);
            return Ok(resume);
        }
        //[HttpPost]
        //public async Task<IActionResult> CreateAsync([FromForm] CandidateResumePostDTO entity)
        //{
        //    await _candidateResumeService.CreateCandidateResumeAsync(entity);
        //    return Ok("Candidate resume created");

        //}
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id,[FromForm] CandidateResumePutDTO resume)
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
        [HttpGet("AppliedVacancies")]
        public async Task<IActionResult> ViewAppliedJobs(int candidateId)
        {
           var appliedJobs=await _candidateResumeService.ViewAppliedJobs(candidateId);
            return Ok(appliedJobs);
        }
    }
}
