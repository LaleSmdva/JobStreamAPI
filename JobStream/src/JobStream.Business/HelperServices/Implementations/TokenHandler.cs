using JobStream.Business.DTOs.LoginToken;
using JobStream.Business.HelperServices.Interfaces;
using JobStream.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.HelperServices.Implementations
{
	public class TokenHandler : ITokenHandler
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IConfiguration _configuration;

		public TokenHandler(UserManager<AppUser> userManager, IConfiguration configuration)
		{
			_userManager = userManager;
			_configuration = configuration;
		}



		public async Task<LoginTokenResponseDTO> GenerateTokenAsync(AppUser user, int hours)
		{
			LoginTokenResponseDTO tokenn = new();

			List<Claim> claims = new List<Claim>()
			{
				new Claim("Fullname",user.Fullname),
				new Claim(ClaimTypes.Name,user.UserName),
				new Claim(ClaimTypes.Email,user.Email),
				new Claim(ClaimTypes.NameIdentifier,user.Id),
			};

			var roles = await _userManager.GetRolesAsync(user);
			foreach (var role in roles)
			{
				new Claim(ClaimTypes.Role, role);
			}

			SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(_configuration["JWT:SecurityKey"]));
			SigningCredentials signingCredentials = new(
				key: symmetricSecurityKey,
				algorithm: SecurityAlgorithms.HmacSha256
				);

			JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
				_configuration["JWT:Issuer"],
				_configuration["JWT:Audience"],
				claims,
				DateTime.UtcNow,
				DateTime.UtcNow.AddHours(hours),
				signingCredentials
				);

			JwtSecurityTokenHandler securityTokenHandler = new JwtSecurityTokenHandler();
			string token = securityTokenHandler.WriteToken(jwtSecurityToken);

			//tokenn.RefreshToken =await  GenerateRefreshToken();
			string refreshToken = await GenerateRefreshToken();

			return new()
			{
				Token = token,
				Expires = jwtSecurityToken.ValidTo,
				Username = user.UserName,
				RefreshToken = refreshToken
			};

		}
		public async Task<string> GenerateRefreshToken()
		{
			byte[] number = new byte[32];
			using RandomNumberGenerator random = RandomNumberGenerator.Create();
			random.GetBytes(number);
			return Convert.ToBase64String(number);
		}
		public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate)
		{
		
			if (user != null)
			{
				user.RefreshToken = refreshToken;
				user.RefreshTokenExpires = accessTokenDate.AddMinutes(addOnAccessTokenDate);
				await _userManager.UpdateAsync(user);
			}
			else throw new Exception();

		}
	}
}
