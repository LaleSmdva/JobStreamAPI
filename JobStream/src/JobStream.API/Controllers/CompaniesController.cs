using JobStream.Business.DTOs.CompanyDTO;
using JobStream.Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobStream.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CompaniesController : ControllerBase
	{
		//Companies/GetAll

		//admin
		//Companies//AddCompany,Update,Delete

		private readonly ICompanyService _companyService;

		public CompaniesController(ICompanyService companyService)
		{
			_companyService = companyService;
		}

		[HttpGet("")]
		public IActionResult GetAllCompanies()
		{
			var companies = _companyService.GetAll();
			return Ok(companies);
		}
		//admin

		[HttpPost("create")]
		public async Task<IActionResult> AddCompany(CompanyDTO company)
		{
			await _companyService.CreateAsync(company);
			return Ok();
		
		}

		[HttpPut("update/{id}")]
		public async Task<IActionResult> UpdateCompany(int id,CompanyUpdateDTO company)
		{
			await _companyService.Update(id,company);
			return Ok("Successfully updated");
		}

		[HttpDelete("delete/{id}")]
		public async Task<IActionResult> DeleteCompany(int id)
		{
			await _companyService.Delete(id);
			return Ok("Successfully deleted");
		}
	}
}
