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
				return StatusCode((int)HttpStatusCode.InternalServerError);
			}
		}

	
		/// <summary>
		/// //
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		[Route("forgotpassword")]
		public async Task<IActionResult> ForgotPassword(ForgotPasswordDTO forgotPasswordDTO)
		{
			try
			{
				var token = await _authService.ForgotPassword(forgotPasswordDTO);
				// Send the reset password link to the user's email using a mailing service
				return Ok(token);
			}
			catch (NotFoundException ex)
			{
				return NotFound();
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError);
			}
		}

		[HttpPost]
		[Route("resetpassword")]
		public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPasswordDTO, string userId, string token)
		{
			try
			{
				var result = await _authService.ResetPassword(resetPasswordDTO, userId, token);
				return Ok(result);
			}
			catch (BadRequestException ex)
			{
				return BadRequest();
			}
			catch (NotFoundException ex)
			{
				return NotFound();
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError);
			}
		}

		//[HttpPost("[action]")]

		//public async Task<IActionResult> ConfirmEmail( string token,string userId)
		//{
		//	await _authService.ConfirmEmail(token,userId);
		//	return Ok();
		//}

		//[HttpPost("[action]")]
		//public async Task<IActionResult> LoginCompany(LoginCompanyDTO loginCompanyDTO)
		//{
		//	await _authService.LoginCompany(loginCompanyDTO);
		//	return Ok("Logged in");
		//}
	}
}
