using JobStream.Business.DTOs.CompanyDTO;
using JobStream.Business.DTOs.InvitationDTO;
using JobStream.Business.DTOs.VacanciesDTO;
using JobStream.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobStream.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompaniesController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCompanies()
        {
            var companies = await _companyService.GetAllAsync();
            return Ok(companies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompanyById(int id)
        {
            var companies = await _companyService.GetByIdAsync(id);
            return Ok(companies);
        }
        [HttpGet("Search")]
        public IActionResult GetCompanyByName([FromQuery]string companyName)
        {
            var companies = _companyService.GetCompaniesByName(companyName);
            return Ok(companies);
        }

    

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(string id, [FromQuery] List<int> deletedCategoryId, [FromForm] CompanyPutDTO companyPutDTO)
        {
            await _companyService.UpdateCompanyAccount(id, deletedCategoryId, companyPutDTO);
            return Ok("Successfully updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            await _companyService.DeleteCompany(id);
            return Ok("Successfully deleted");
        }

        [HttpPost("{companyId}/AddVacancy")]
        public async Task<IActionResult> AddVacancyToCompany(int companyId,[FromBody] VacanciesPostDTO vacanciesPostDTO)
        {
            await _companyService.AddVacancyToCompany(companyId, vacanciesPostDTO);
            return Ok("Successfully added vacancy");

        }
        [HttpPut("{companyId}/vacancies/{vacancyId}")]
        public async Task<IActionResult> UpdateVacancy(int companyId, int vacancyId,[FromBody] VacanciesPutDTO vacanciesPutDTO)
        {
            await _companyService.UpdateVacancy(companyId,vacancyId, vacanciesPutDTO);
            return Ok("Vacancy updated");
        }
        [HttpDelete("{companyId}/vacancies/{vacancyId}")]
        public async Task<IActionResult> DeleteVacancy(int companyId, int vacancyId)
        {
            await _companyService.DeleteVacancy(companyId, vacancyId);
            return Ok("Successfully deleted vacancy");
        }
        //[HttpPost("[action]/{vacancyId}/{candidateId}")]
        //public async Task<IActionResult> InviteCandidateToInterview(int vacancyId, int candidateId, [FromBody] InvitationPostDTO invitation)
        //{
        //    await _companyService.InviteCandidateToInterview(vacancyId, candidateId, invitation);
        //    return Ok("Invitation sent successfully");
        //}
        [HttpPost("{vacancyId}/invite/{candidateId}")]
        public async Task<IActionResult> InviteCandidateToInterview(int vacancyId, int candidateId, [FromBody] InvitationPostDTO invitation)
        {
            await _companyService.InviteCandidateToInterview(vacancyId, candidateId, invitation);
            return Ok("Invitation sent successfully");
        }

        [HttpPost("{vacancyId}/reject/{candidateId}")]
        public async Task<IActionResult> RejectCandidate(int vacancyId, int candidateId)
        {
            await _companyService.RejectCandidate(vacancyId, candidateId);
            return Ok("Candidate rejected");
        }

        [HttpGet("{companyId}/Applications")]
        public async Task<IActionResult> GetAllApplications(int companyId)
        {
            var list=await _companyService.GetAllApplications(companyId);
            return Ok(list);
        }
    }
}

