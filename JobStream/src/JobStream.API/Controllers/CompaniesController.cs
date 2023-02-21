using JobStream.Business.DTOs.CompanyDTO;
using JobStream.Business.Exceptions;
using JobStream.Business.Services.Interfaces;
using JobStream.Business.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace JobStream.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	//[Authorize]
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
		public  IActionResult GetAllCompanies()
		{
			try
			{
				var companies = _companyService.GetAllAsync();
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
			try
			{
				var companies = await _companyService.GetByIdAsync(id);
				return Ok(companies);
			}
			catch (NotFoundException ex)
			{

				return NotFound(ex.Message);
			}
		}
		//admin

		[HttpPost("create")]
		public async Task<IActionResult> AddCompany([FromForm]CompanyPostDTO company)
		{

			try
			{
				//var existingCompany = await _companyService.GetByIdAsync(company.Id);
				var companies = _companyService.GetAllAsync();
		
				if (!company.Logo.CheckFileFormat("image/"))
				{
					throw new FormatException("File format is not supported, insert an image file");
				}
				if (company.Logo.CheckFileSize(2))
				{
					throw new FormatException("Max file size is 2 MB");
				}
				//if (company.Id != 0 || company.Id)
				//{
				//	throw new InvalidOperationException("You can not insert id value.");
				//}
				//if (existingCompany.Id != null)
				//{
				//	throw new BadRequestException("A company with the same id already exists.");
				//}
				if (companies.Any(c => c.Name == company.Name && c.Email == company.Email))
				{
					throw new BadRequestException("A company with the same name and email already exists");
				}

				await _companyService.CreateAsync(company);
				return Ok("Successfully created");
			}
			catch (FormatException ex)
			{
				return BadRequest(ex.Message);
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
		public async Task<IActionResult> UpdateCompany(int id, [FromForm] CompanyPutDTO company)
		{
			try
			{
				await _companyService.Update(id, company);
				return Ok("Successfully updated");
			}
			catch (BadRequestException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (NotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch (Exception)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError);

			}
		}

		[HttpDelete("delete/{id}")]
		public async Task<IActionResult> DeleteCompany(int id)
		{
			try
			{
				await _companyService.Delete(id);
				return Ok("Successfully deleted");
			}
			catch (NotFoundException ex)
			{

				return NotFound(ex.Message);
			}
		}
	}
}
