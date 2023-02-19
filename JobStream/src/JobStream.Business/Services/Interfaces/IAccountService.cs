using JobStream.Business.DTOs.Account;
using JobStream.Core.Entities.Identity;
using JobStream.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Services.Interfaces
{
	public interface IAccountService
	{
		List<AppUserDTO> GetAllCandidateAccounts();
		Task<AppUserDTO> GetCandidateAccountByUsernameAsync(string userName);
		Task RegisterCandidate(RegisterCandidateDTO registerCandidate);
		Task<bool> CreateRoleAsync(string userName, List<string> roles);
		//Task<bool> UpdateRoleAsync(string userName, List<string> newRoles,List<string> deletedRoles);

		Task RegisterCompany(RegisterCompanyDTO registerCompany);
		//Task<string> SendConfirmationEmailAsync(string emailAddress, string confirmationLink);

	}
}
