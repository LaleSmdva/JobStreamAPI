﻿using JobStream.Business.DTOs.Account;
using JobStream.Business.DTOs.LoginToken;
using JobStream.Business.Exceptions;
using JobStream.Business.HelperServices.Interfaces;
using JobStream.Business.Services.Interfaces;
using JobStream.Core.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JobStream.Business.Services.Implementations
{
	public class AuthService:IAuthService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly IConfiguration _configuration;
		private readonly ITokenHandler _tokenHandler;
		//private readonly IUrlHelper _urlHelper;
		//private readonly HttpContext _httpContext;

		public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration, ITokenHandler tokenHandler)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_configuration = configuration;
			_tokenHandler = tokenHandler;
			
		}



		public async Task<LoginTokenResponseDTO> LoginCandidateAsync(LoginCandidateDTO loginCandidate)
		{
			var user = await _userManager.FindByEmailAsync(loginCandidate.UsernameOrEmail);
			if (user == null)
			{
				user = await _userManager.FindByNameAsync(loginCandidate.UsernameOrEmail);
				if (user == null)
				{
					throw new BadRequestException("Username/Email or password in incorrect");
				}
			}
			//change password
			//await _userManager.ChangePasswordAsync(user, loginCandidate.Password);
			var checkPassword = await _userManager.CheckPasswordAsync(user, loginCandidate.Password);
		
			if (!checkPassword)
			{
				throw new BadRequestException("Username/Email or password in incorrect");
			}

			var response=await _tokenHandler.GenerateTokenAsync(user,3);
			await _tokenHandler.UpdateRefreshToken(response.RefreshToken, user, response.Expires, 5);
			return response;
			
		}
		
		public async Task<string> ForgotPassword(ForgotPasswordDTO forgotPasswordDTO)
		{


			var user = await _userManager.FindByEmailAsync(forgotPasswordDTO.Email);
			if (user == null)
			{
				throw new NotFoundException("Email not found");
			}
			var token = await _userManager.GeneratePasswordResetTokenAsync(user);
			//var resetLink = UrlHelperExtensions.Action("ResetPassword", "Auth", new { userId = user.Id, token = token }, "https://");



			//throw new NotImplementedException();
			return token;
		}


		public async Task<string> ResetPassword(ResetPasswordDTO resetPasswordDTO,string userId,string token)
		{
			if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token)) throw new BadRequestException("");
			var user = await _userManager.FindByIdAsync(userId);
			if (user is null) throw new  NotFoundException("User not found");

			var identityResult=await _userManager.ResetPasswordAsync(user, token, resetPasswordDTO.Newpassword);
			if (!identityResult.Succeeded)
			{
				throw new Exception();
			}

			return "Password reset successfully.";
		}



		//public async Task LoginCompany(LoginCompanyDTO loginCompany)
		//{

		//}
	}
}