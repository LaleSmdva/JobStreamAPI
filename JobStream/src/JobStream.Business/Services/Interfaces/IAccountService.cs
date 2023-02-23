using JobStream.Business.DTOs.Account;
using JobStream.Core.Entities.Identity;
using JobStream.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C = JobStream.Business.DTOs.Account;

namespace JobStream.Business.Services.Interfaces
{
	public interface IAccountService
	{
		List<CandidateDTO> GetAllCandidateAccounts();
		List<C.CompanyDTO> GetAllCompanyAccounts();
		Task<AppUserDTO> GetCandidateAccountByUsernameAsync(string userName);
		Task RegisterCandidate(RegisterCandidateDTO registerCandidate);

		Task<List<object>> GetAllRolesAsync();
		Task<bool> CreateRoleAsync(string userName, List<string> roles);
		Task<bool> UpdateRoleAsync(string userName, List<string> newRoles, List<string> deletedRoles);

		Task RegisterCompany(RegisterCompanyDTO registerCompany);
		//Task<string> SendConfirmationEmailAsync(string emailAddress, string confirmationLink);

	}
}
