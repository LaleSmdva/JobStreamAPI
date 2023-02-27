using JobStream.Business.DTOs.CompanyDTO;
using JobStream.Business.DTOs.InvitationDTO;
using JobStream.Business.DTOs.VacanciesDTO;
using JobStream.Business.Exceptions;
using JobStream.Business.Services.Interfaces;
using JobStream.Business.Utilities;
using JobStream.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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

        [HttpGet("Company/{id}")]
        public async Task<IActionResult> GetCompanyById(int id)
        {
            var companies = await _companyService.GetByIdAsync(id);
            return Ok(companies);
        }
        [HttpGet("Company/{companyName}")]
        public IActionResult GetCompanyByName(string companyName)
        {
            var companies = _companyService.GetCompaniesByName(companyName);
            return Ok(companies);
        }

        //[HttpPost("create")]
        //public async Task<IActionResult> AddCompany([FromForm] CompanyPostDTO company)
        //{
        //    await _companyService.CreateAsync(company);
        //    return Ok("Successfully created");
        //}


        [HttpPut("UpdateCompanyAccount/{id}")]
        public async Task<IActionResult> UpdateCompany(string id, [FromQuery] List<int> addedCategoryId, [FromQuery] List<int> deletedCategoryId, [FromForm] CompanyPutDTO companyPutDTO)
        {
            await _companyService.UpdateCompanyAccount(id, addedCategoryId, deletedCategoryId, companyPutDTO);
            return Ok("Successfully updated");
        }

        [HttpDelete("DeleteAccount/{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            await _companyService.DeleteCompany(id);
            return Ok("Successfully deleted");
        }

        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> AddVacancy(int id, VacanciesPostDTO vacanciesPostDTO)
        {
            await _companyService.AddVacancyToCompany(id, vacanciesPostDTO);
            return Ok("Successfully added vacancy");

        }
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateVacancy(int id, VacanciesPutDTO vacanciesPutDTO)
        {
            await _companyService.UpdateVacancy(id, vacanciesPutDTO);
            return Ok("Vacancy updated");
        }
        [HttpDelete("[action]/{id}/{vacancyId}")]
        public async Task<IActionResult> DeleteVacancy(int id, int vacancyId)
        {
            await _companyService.DeleteVacancy(id, vacancyId);
            return Ok("Successfully deleted vacancy");
        }
        [HttpPost("[action]/{vacancyId}/{candidateId}")]
        public async Task<IActionResult> InviteCandidateToInterview(int vacancyId, int candidateId, InvitationPostDTO invitation)
        {
            await _companyService.InviteCandidateToInterview(vacancyId, candidateId, invitation);
            return Ok("Invitation sent successfully");
        }

        [HttpPost("[action]/{vacancyId}/{candidateId}")]
        public async Task<IActionResult> RejectCandidate(int vacancyId, int candidateId)
        {
            await _companyService.RejectCandidate(vacancyId, candidateId);
            return Ok("Candidate rejected");
        }
    }
}

