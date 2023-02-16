using JobStream.Business.DTOs.Account;
using JobStream.Business.Exceptions;
using JobStream.Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace JobStream.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("[action]")]
		public async Task<IActionResult> CandidateLogin(LoginCandidateDTO loginCandidateDTO)
		{
			try
			{
				var tokenResponse = await _authService.LoginCandidateAsync(loginCandidateDTO);
				return Ok(tokenResponse);
			}
			catch (BadRequestException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception)
			{
				return StatusCode((int) HttpStatusCode.InternalServerError);
			}
		}

		//[HttpPost("[action]")]
		//public async Task<IActionResult> LoginCompany(LoginCompanyDTO loginCompanyDTO)
		//{
		//	await _authService.LoginCompany(loginCompanyDTO);
		//	return Ok("Logged in");
		//}
	}
}
