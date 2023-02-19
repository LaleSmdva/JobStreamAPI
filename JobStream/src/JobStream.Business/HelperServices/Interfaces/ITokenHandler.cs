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
		Task<LoginTokenResponseDTO> GenerateTokenAsync(AppUser user,int hours);
		Task<string> GenerateRefreshToken();
		Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate);
	}
}
