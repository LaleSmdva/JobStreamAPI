using JobStream.Business.DTOs.Account;
using JobStream.Business.DTOs.LoginToken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Services.Interfaces
{
	public interface IAuthService
	{
		Task<LoginTokenResponseDTO> LoginCandidateAsync(LoginCandidateDTO loginCandidate);
		//Task LoginCompanyAsync(LoginCompanyDTO loginCompany);
	}
}
