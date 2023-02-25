using JobStream.Business.DTOs.CompanyDTO;
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

        [HttpGet("")]
        public async Task<IActionResult> GetAllCompanies()
        {
            var companies = await _companyService.GetAllAsync();
            return Ok(companies);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCompanyById(int id)
        {
            var companies = await _companyService.GetByIdAsync(id);
            return Ok(companies);
        }
        [HttpGet("[action]")]
        public IActionResult GetCompanyByName(string companyName)
        {
            var companies = _companyService.GetCompaniesByName(companyName);
            return Ok(companies);
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddCompany([FromForm] CompanyPostDTO company)
        {
            await _companyService.CreateAsync(company);
            return Ok("Successfully created");
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCompany(int id, [FromQuery] List<int> addedCategoryId, [FromQuery] List<int> deletedCategoryId, [FromForm] CompanyPutDTO companyPutDTO)
        {
            await _companyService.Update(id, addedCategoryId, deletedCategoryId, companyPutDTO);
            return Ok("Successfully updated");
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            await _companyService.Delete(id);
            return Ok("Successfully deleted");
        }

        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> AddVacancy(int id, VacanciesPostDTO vacanciesPostDTO)
        {
            await _companyService.AddVacancy(id, vacanciesPostDTO);
            return Ok("Successfully added vacancy");
        }

        [HttpDelete("delete/{id}/{vacancyId}")]
        public async Task<IActionResult> DeleteVacancy(int id, int vacancyId)
        {
            await _companyService.DeleteVacancy(id, vacancyId);
            return Ok("Successfully deleted vacancy");
        }
    }
}
