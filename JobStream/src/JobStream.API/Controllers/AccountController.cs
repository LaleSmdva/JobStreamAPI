using JobStream.Business.DTOs.Account;
using JobStream.Business.Exceptions;
using JobStream.Business.Services.Interfaces;
using JobStream.Business.Validators.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

namespace JobStream.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }



        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllCandidateAccounts()
        {
            var accounts = await _accountService.GetAllCandidateAccounts();
            return Ok(accounts);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllCompanyAccounts()
        {
            var accounts = await _accountService.GetAllCompanyAccounts();
            return Ok(accounts);
        }

        //[HttpGet("[action]")]
        [HttpGet("candidate/{userName}")]
        public async Task<IActionResult> GetCandidateAccountByUsername(string userName)
        {
            var user = await _accountService.GetCandidateAccountByUsernameAsync(userName);
            return Ok(user);
        }
        [HttpGet("company/{companyName}")]
        public async Task<IActionResult> GetCompanyAccountByCompanyNameAsync(string companyName)
        {
            var user = await _accountService.GetCompanyAccountByCompanyNameAsync(companyName);
            return Ok(user);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RegisterCandidate(RegisterCandidateDTO candidateDto)
        {
            await _accountService.RegisterCandidate(candidateDto);
            return Ok("Candidate registered");
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetUserRoles()
        {
            var list = await _accountService.GetAllRolesAsync();
            return Ok(list);
        }


        [HttpPost("{userId}/[action]")]
        //[Authorize(Roles = "Admin,Moderator")]

        public async Task<IActionResult> CreateRole(string userId, [FromQuery] List<string> roles)
        {
            await _accountService.CreateRoleAsync(userId, roles);
            return Ok("Role added successfully");

        }

        [HttpPut("{userId}/[action]")]
        //[Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> UpdateRole(string userId, [FromQuery] List<string> newRoles, [FromQuery] List<string> deletedRoles)
        {
            await _accountService.UpdateRoleAsync(userId, newRoles, deletedRoles);
            return Ok("Role updated successfully");
        }
        [HttpGet("{id}/roles")]
        //[Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> GetRolesById(string id)
        {
            var roles=await _accountService.GetRolesById(id);
            return Ok(roles);
        }
      

        [HttpPost("[action]")]
        public async Task<IActionResult> RegisterCompany(RegisterCompanyDTO companyDto)
        {
            await _accountService.RegisterCompany(companyDto);
            return Ok("Company registered");
        }

    

    }
}
