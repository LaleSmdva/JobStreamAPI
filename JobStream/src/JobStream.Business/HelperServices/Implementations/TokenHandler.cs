﻿using JobStream.Business.DTOs.LoginToken;
using JobStream.Business.HelperServices.Interfaces;
using JobStream.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

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



        public async Task<LoginTokenResponseDTO> GenerateTokenAsync(AppUser user)
        {
            //LoginTokenResponseDTO tokenn = new();

            List<Claim> claims = new List<Claim>()
            {
				//new Claim("Fullname",user.Fullname),
				new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
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
                DateTime.Now,
                //DateTime.Now.AddHours(5),
                DateTime.Now.AddMinutes(243),
                signingCredentials
                );

            JwtSecurityTokenHandler securityTokenHandler = new JwtSecurityTokenHandler();
            string token = securityTokenHandler.WriteToken(jwtSecurityToken);

            //tokenn.RefreshToken =await  GenerateRefreshToken();
            //string refreshToken = await GenerateRefreshToken();

            return new()
            {
                Token = token,
                Expires = jwtSecurityToken.ValidTo,
                Username = user.UserName,
                //RefreshToken = refreshToken
            };

        }

        public async Task<string> GenerateRefreshToken(AppUser user,DateTime expires)
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            var refreshToken = Convert.ToBase64String(randomNumber);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpires = expires;

            await _userManager.UpdateAsync(user);

            return refreshToken;
            //	byte[] number = new byte[32];
            //	using RandomNumberGenerator random = RandomNumberGenerator.Create();
            //	random.GetBytes(number);
            //	return Convert.ToBase64String(number);
        }
        //public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate)
        //{

        //	if (user != null)
        //	{
        //		user.RefreshToken = refreshToken;
        //		user.RefreshTokenExpires = accessTokenDate.AddHours(addOnAccessTokenDate);
        //		await _userManager.UpdateAsync(user);
        //	}
        //	else throw new Exception();

        //}
    }
}
