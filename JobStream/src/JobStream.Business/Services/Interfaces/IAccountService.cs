using JobStream.Business.DTOs.Account;
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
		Task RegisterCandidate(RegisterCandidateDTO registerCandidate);
		Task RegisterCompany(RegisterCompanyDTO registerCompany);
	}
}
