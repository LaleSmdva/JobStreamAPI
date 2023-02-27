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
        private readonly IMailService _mailService;

        public AuthController(IAuthService authService, IMailService mailService)
        {
            _authService = authService;
            _mailService = mailService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CandidateLogin(LoginCandidateDTO loginCandidateDTO)
        {
            var tokenResponse = await _authService.LoginCandidateAsync(loginCandidateDTO);
            return Ok(tokenResponse);
        }


        /// <summary>
        /// //
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("forgotpassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDTO forgotPasswordDTO)
        {
            var token = await _authService.ForgotPassword(forgotPasswordDTO);
            // Send the reset password link to the user's email using a mailing service
            return Ok(token);
        }

        [HttpPost]
        [Route("resetpassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPasswordDTO, string userId, string token)
        {
            var result = await _authService.ResetPassword(resetPasswordDTO, userId, token);
            return Ok(result);
        }
        [HttpPost]
        [Route("emailresetpassword")]
        public async Task<IActionResult> EmailResetPassword([FromForm] MailRequestDTO mailRequestDTO)
        {
            await _mailService.SendEmailAsync(mailRequestDTO);
            return Ok();
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
