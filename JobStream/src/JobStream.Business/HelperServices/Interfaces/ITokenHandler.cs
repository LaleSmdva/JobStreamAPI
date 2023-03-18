using JobStream.Business.DTOs.LoginToken;
using JobStream.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.HelperServices.Interfaces
{
	public interface ITokenHandler
	{
		Task<LoginTokenResponseDTO> GenerateTokenAsync(AppUser user);
		Task<string> GenerateRefreshToken(AppUser user,DateTime expires);
		//Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate);
	}
}
