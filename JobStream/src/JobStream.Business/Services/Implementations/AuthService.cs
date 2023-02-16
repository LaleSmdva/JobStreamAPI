using JobStream.Business.DTOs.Account;
using JobStream.Business.DTOs.LoginToken;
using JobStream.Business.Exceptions;
using JobStream.Business.HelperServices.Interfaces;
using JobStream.Business.Services.Interfaces;
using JobStream.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Services.Implementations
{
	public class AuthService:IAuthService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly IConfiguration _configuration;
		private readonly ITokenHandler _tokenHandler;

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
			return response;
			
		}
		//public async Task LoginCompany(LoginCompanyDTO loginCompany)
		//{

		//}
	}
}
