using JobStream.Business.DTOs.Account;
using JobStream.Business.Exceptions;
using JobStream.Business.Services.Interfaces;
using JobStream.Core.Entities.Identity;
using JobStream.Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Services.Implementations
{
	public class AccountService : IAccountService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly IConfiguration _configuration;

		public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_configuration = configuration;
		}

		public async Task RegisterCandidate(RegisterCandidateDTO registerCandidate)
		{

			AppUser candidate = new()
			{
				Fullname = registerCandidate.Fullname,
				UserName = registerCandidate.Username,
				Email = registerCandidate.Email,
			};
			if (await _userManager.Users.AnyAsync(u => u.UserName == candidate.UserName))
			{
				throw new DuplicateUserNameException("User with the same username already exists");
			}
			if (await _userManager.Users.AnyAsync(u => u.Email == candidate.Email))
			{
				throw new DuplicateEmailException("User with the same email address already exists");
			}

			var identityResult = await _userManager.CreateAsync(candidate, registerCandidate.Password);

			if (!identityResult.Succeeded)
			{
				var errorMessages = new List<string>();
				foreach (var error in identityResult.Errors)
				{
					errorMessages.Add(error.Description);
				}
				throw new CreateUserFailedException($"{errorMessages}");
				//return BadRequest(errorMessages);
			}

			var result = await _userManager.AddToRoleAsync(candidate, UserRoles.Candidate.ToString());
			if (!result.Succeeded)
			{
				var errorMessages = new List<string>();
				foreach (var error in result.Errors)
				{
					errorMessages.Add(error.Description);
				}
				throw new CreateRoleFailedException($"{errorMessages}");
			}

		}

		public async Task RegisterCompany(RegisterCompanyDTO registerCompany)
		{
			AppUser company = new()
			{
				Companyname = registerCompany.Companyname,
				Email = registerCompany.Email,
				InfoCompany = registerCompany.InfoCompany,

			};
			if (await _userManager.Users.AnyAsync(u => u.UserName == company.UserName))
			{
				throw new DuplicateUserNameException("User with the same username already exists");
			}
			if (await _userManager.Users.AnyAsync(u => u.Email == company.Email))
			{
				throw new DuplicateEmailException("User with the same email address already exists");
			}
			var identityResult = await _userManager.CreateAsync(company, registerCompany.Password);
			if (!identityResult.Succeeded)
			{
				var errorMessages = new List<string>();
				foreach (var error in identityResult.Errors)
				{
					errorMessages.Add(error.Description);
				}
				throw new CreateUserFailedException($"{errorMessages}");
				//return BadRequest(errorMessages);
			}

			var result = await _userManager.AddToRoleAsync(company, UserRoles.Company.ToString());
			if (!result.Succeeded)
			{
				var errorMessages = new List<string>();
				foreach (var error in result.Errors)
				{
					errorMessages.Add(error.Description);
				}
				throw new CreateRoleFailedException($"{errorMessages}");
			}

		}

		
	}
}
