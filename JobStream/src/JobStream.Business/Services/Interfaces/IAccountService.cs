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
       
		Task<List<CandidateDTO>> GetAllCandidateAccounts();
		Task<List<C.CompanyDTO>> GetAllCompanyAccounts();
		Task<CandidateDTO> GetCandidateAccountByUsernameAsync(string userName);
		Task<C.CompanyDTO> GetCompanyAccountByCompanyNameAsync(string companyName);

        Task RegisterCandidate(RegisterCandidateDTO registerCandidate);

		Task<List<object>> GetAllRolesAsync();
		Task<bool> CreateRoleAsync(string userId, List<string> roles);
		Task<bool> UpdateRoleAsync(string userId, List<string> newRoles, List<string> deletedRoles);
		Task<List<string>> GetRolesById(string id);

		Task RegisterCompany(RegisterCompanyDTO registerCompany);
		//Task<string> SendConfirmationEmailAsync(string emailAddress, string confirmationLink);

	}
}
