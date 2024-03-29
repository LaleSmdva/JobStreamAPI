﻿using JobStream.Business.DTOs.Account;
using JobStream.Business.DTOs.LoginToken;
using JobStream.Business.Exceptions;
using JobStream.Business.HelperServices.Interfaces;
using JobStream.Business.Services.Interfaces;
using JobStream.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Web;

namespace JobStream.Business.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenHandler _tokenHandler;
        private readonly IMailService _mailService;
        //private readonly IUrlHelper _urlHelper;
        //private readonly HttpContext _httpContext;

        public AuthService(UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager,ITokenHandler tokenHandler, IMailService mailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
            _mailService = mailService;
        }

        public async Task<LoginTokenResponseDTO> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.UsernameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(loginDTO.UsernameOrEmail);
                if (user == null)
                {
                    throw new BadRequestException("Username/Email or password in incorrect");
                }
            }
            //change password
            //await _userManager.ChangePasswordAsync(user, loginCandidate.Password);
            var checkPassword = await _userManager.CheckPasswordAsync(user, loginDTO.Password);

            if (!checkPassword)
            {
                throw new BadRequestException("Username/Email or password in incorrect");
            }

            LoginTokenResponseDTO response = await _tokenHandler.GenerateTokenAsync(user);
            var refreshToken = await _tokenHandler.GenerateRefreshToken(user,DateTime.Now.AddHours(4));
            //await _tokenHandler.UpdateRefreshToken(response.RefreshToken, user, response.Expires, 5);

            //return new LoginTokenResponseDTO
            //{
            //    //AccessToken=response,
            //    Expires= DateTime.UtcNow,
            //    Username= user.UserName,
            //    RefreshToken= $"Refresh token:{refreshToken}////Access token:{response}",

            //};
            return new LoginTokenResponseDTO
            {
                Token = response.Token,
                Expires= response.Expires,
                RefreshToken = refreshToken
            };

        }

        public async Task<string> ForgotPassword(ForgotPasswordDTO forgotPasswordDTO)
        {


            var user = await _userManager.FindByEmailAsync(forgotPasswordDTO.Email);
            if (user == null)
            {
                throw new NotFoundException("Email not found");
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            /// custom link
            string baseUrl = "https://localhost:7101/";
            string resetPasswordPath = "api/Auth/resetpassword";

            string resetPasswordLink = $"{baseUrl}{resetPasswordPath}?userId={user.Id}&token={token}";
            ///
            await _mailService.SendEmailAsync(new MailRequestDTO
            {
                ToEmail = user.Email,
                Subject = "Reset password",
                Body = $"<a href={resetPasswordLink}>Click this link to reset your password</a>"
            });


            //var callbackUrl = Url.Link("ResetPasswordRoute", new { email = user.Email, token = token });
            //var resetLink = UrlHelperExtensions.Action("ResetPassword", "Auth", new { userId = user.Id, token = token }, "https://");
            //var response = new HttpResponseMessage(HttpStatusCode.OK);
        

            return $"Token:{token}";

        }


        public async Task<string> ResetPassword(ResetPasswordDTO resetPasswordDTO, string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token)) throw new BadRequestException("");
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null) throw new NotFoundException("User not found");

            var identityResult = await _userManager.ResetPasswordAsync(user, token, resetPasswordDTO.Newpassword);
            if (!identityResult.Succeeded)
            {
                throw new Exception();
            }
            return "Password reset successfully.";
        }

    }
}
