using JobStream.Business.Services.Interfaces;
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

        [HttpGet("details")]
        public async Task<IActionResult> GetCandidateResumeDetails(int candidateId)
        {
            var candidate=await _candidateResumeService.CandidateResumeDetails(candidateId);
            return Ok(candidate);
        }
    }
}
