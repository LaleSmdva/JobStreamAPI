﻿using JobStream.Business.DTOs.Account;
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
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JobStream.DataAccess.Repositories.Interfaces;
using AutoMapper;
using ArgumentNullException = JobStream.Business.Exceptions.ArgumentNullException;

namespace JobStream.Business.Services.Implementations
{
	public class AccountService : IAccountService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly IConfiguration _configuration;
		private readonly IAccountRepository _accountRepository;
		private readonly IMapper _mapper;
		private readonly RoleManager<IdentityRole> _roleManager;

		public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration, IAccountRepository accountRepository, IMapper mapper, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_configuration = configuration;
			_accountRepository = accountRepository;
			_mapper = mapper;
			_roleManager = roleManager;
		}

		public List<AppUserDTO> GetAllCandidateAccounts()
		{

			var accounts = _accountRepository.GetAll().ToList();
			var result = _mapper.Map<List<AppUserDTO>>(accounts);
			return result;
		}

		public async Task<AppUserDTO> GetCandidateAccountByUsernameAsync(string userName)
		{
			var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userName);
			if (user == null) throw new NotFoundException("User not found");
			var response = _mapper.Map<AppUserDTO>(user);
			return response;
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
			//////// NEW  ///////
			var token = await _userManager.GenerateEmailConfirmationTokenAsync(candidate);
			//var confirmationLink = Url

		}


		public async Task<bool> CreateRoleAsync(string userName, List<string> roles)
		{

			if (string.IsNullOrWhiteSpace(userName)) throw new ArgumentNullException("Username is required");
			var User = await _userManager.FindByNameAsync(userName);

			List<string> allRoles = new();

			foreach (var rolee in Enum.GetValues(typeof(UserRoles)))
			{
				allRoles.Add(rolee.ToString());
			}

			foreach (var role in roles)
			{
				if (!allRoles.Contains(role))
				{
					throw new NotFoundException("There is no such a role");
				}

				var newRole = new IdentityRole
				{
					Name = role,
				};
				await _roleManager.CreateAsync(newRole);

				var identityResult = await _userManager.AddToRoleAsync(User, role);

				//var errorMessages = new List<string>();
				//foreach (var error in identityResult.Errors)
				//{
				//	errorMessages.Add(error.Description);
				//}
				//throw new CreateRoleFailedException($"{role} role already exists");



			}
			return true;
		}


		//public Task<bool> UpdateRoleAsync(string userName, List<string> roles)
		//{
		//	throw new NotImplementedException();
		//}

		//[HttpGet("confirm-email")]
		//public async Task<string> SendConfirmationEmailAsync(string emailAddress, string confirmationLink)
		//{
		//	var message = new MailMessage();
		//	message.To.Add(new MailAddress(emailAddress));
		//	message.Subject = "Confirm Your Registration";
		//	message.Body = $"Thank you for registering with our service. To complete the registration process, please click on the following link to verify your email address: {confirmationLink}";

		//	using (var client = new SmtpClient())
		//	{
		//		client.Host = "smtp.gmail.com"; // replace with your email provider's SMTP server address
		//		client.Port = 587; // replace with your email provider's SMTP server port
		//		client.UseDefaultCredentials = false;
		//		client.Credentials = new NetworkCredential("your_email_address@gmail.com", "your_email_password");
		//		client.EnableSsl = true;
		//		await client.SendMailAsync(message);
		//	}
		//	return message.Body;

		//}

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
			///////    new ////
			//var code = await _userManager.GenerateEmailConfirmationTokenAsync(company.Id);
			//var callbackUrl = Url.Link("ConfirmEmailRoute", new { userId = company.Id, code = code });
			//await _userManager.SendEmailAsync(company.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");



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
