using JobStream.Business.DTOs.CompanyDTO;
using JobStream.Business.Exceptions;
using JobStream.Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

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
			try
			{
				var companies = _companyService.GetAll();
				return Ok(companies);
			}
			catch (NotFoundException)
			{

				return NotFound("Not Found");
			}
			//catch (Exception)
			//{
			//	throw new InvalidOperationException("The requested resource could not be found.");	

			//}
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> GetCompanyById(int id)
		{
			var companies = await _companyService.GetById(id);
			return Ok(companies);
		}
		//admin

		[HttpPost("create")]
		public async Task<IActionResult> AddCompany(CompanyPostDTO company)
		{

			try
			{
				var existingCompany = await _companyService.GetById(company.Id);
				var companies = _companyService.GetAll();
				if (existingCompany != null)
				{
					throw new BadRequestException("A company with the same id already exists.");
				}
				if (company.Id != 0)
				{
					throw new InvalidOperationException(" The requested resource could not be found.");
				}
				if (companies.Any(c => c.Name == company.Name && c.Email == company.Email))
				{
					throw new BadRequestException("A company with the same name and email already exists");
				}

				await _companyService.CreateAsync(company);
				return Ok("Successfully created");
			}
			catch (BadRequestException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError);

			}
		}

		[HttpPut("update/{id}")]
		public async Task<IActionResult> UpdateCompany(int id, CompanyPutDTO company)
		{
			await _companyService.Update(id, company);
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
