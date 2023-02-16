using JobStream.Business.DTOs.Account;
using JobStream.Business.Exceptions;
using JobStream.Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace JobStream.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IAccountService _accountService;

		public AccountController(IAccountService accountService)
		{
			_accountService = accountService;
		}

		[HttpPost("[action]")]
		public async Task<IActionResult> RegisterCandidate(RegisterCandidateDTO candidateDto)
		{
			try
			{
				await _accountService.RegisterCandidate(candidateDto);
				return Ok("Candidate registered");
			}
			catch (DuplicateUserNameException ex)
			{
				return BadRequest(ex.Message);
			}
			catch(DuplicateEmailException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (CreateUserFailedException ex)
			{
				return BadRequest(ex.Message);
				//return StatusCode((int)HttpStatusCode.InternalServerError,ex.Message);
			}
			catch (CreateRoleFailedException ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError,ex.Message);
			}
		}

		[HttpPost("[action]")]
		public async Task<IActionResult> RegisterCompany(RegisterCompanyDTO companyDto)
		{
			try
			{
				await _accountService.RegisterCompany(companyDto);
				return Ok("Company registered");
			}
			catch (DuplicateUserNameException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (DuplicateEmailException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (CreateUserFailedException ex)
			{
				return BadRequest(ex.Message);
				//return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
			}
			catch (CreateRoleFailedException ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
			}
		}

		
	}
}
