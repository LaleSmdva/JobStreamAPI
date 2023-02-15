using JobStream.Business.DTOs.Account;
using JobStream.Business.Exceptions;
using JobStream.Business.Services.Interfaces;
using JobStream.Core.Entities.Identity;
using JobStream.Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Services.Implementations
{
	public class AccountService:IAccountService
	{
		private readonly UserManager<AppUser> _userManager;

		public AccountService(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task RegisterCandidate(RegisterCandidateDTO registerCandidate)
		{
			AppUser candidate = new()
			{
				Fullname= registerCandidate.Fullname,
				UserName=registerCandidate.Username,
				Email= registerCandidate.Email,
			};
			var identityResult=await _userManager.CreateAsync(candidate, registerCandidate.Password);
			if(!identityResult.Succeeded)
			{
				var errorMessages= new List<string>();
				foreach (var error in identityResult.Errors)
				{
					errorMessages.Add(error.Description);
				}
				throw new CreateUserFailedException($"{errorMessages}");
				//return BadRequest(errorMessages);
			}

			var result=await _userManager.AddToRoleAsync(candidate,UserRoles.Candidate.ToString());
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
				InfoCompany=registerCompany.InfoCompany,
				
			};
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
