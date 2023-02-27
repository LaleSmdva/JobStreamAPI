using JobStream.Business.DTOs.Account;
using JobStream.Business.Exceptions;
using JobStream.Business.Services.Interfaces;
using JobStream.Business.Validators.Account;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace JobStream.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        //GetAccount//UpdateAccount//DeleteAccount
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("[action]")]
        public IActionResult GetAllUserAccounts()
        {
            var accounts = _accountService.GetAllUserAccounts();
            return Ok(accounts);
        }

        [HttpGet("[action]")]
        public IActionResult GetAllCandidateAccounts()
        {
            var accounts = _accountService.GetAllCandidateAccounts();
            return Ok(accounts);
        }

        [HttpGet("[action]")]
        public IActionResult GetAllCompanyAccounts()
        {
            var accounts = _accountService.GetAllCompanyAccounts();
            return Ok(accounts);
        }

        //[HttpGet("[action]")]
        [HttpGet("candidate/{userName}")]
        public async Task<IActionResult> GetCandidateAccountByUsername(string userName)
        {
            var user = await _accountService.GetCandidateAccountByUsernameAsync(userName);
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


        [HttpPost("[action]")]
        //[ValidateRolesModel] ??
        public async Task<IActionResult> CreateRole(string userName, [FromQuery] List<string> roles)
        {
            bool isCreated = await _accountService.CreateRoleAsync(userName, roles);
            return Ok(isCreated);

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateRole(string userName, [FromQuery] List<string> newRoles, [FromQuery] List<string> deletedRoles)
        {
            bool isUpdated = await _accountService.UpdateRoleAsync(userName, newRoles, deletedRoles);
            return Ok(isUpdated);
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> RegisterCompany(RegisterCompanyDTO companyDto)
        {
            await _accountService.RegisterCompany(companyDto);
            return Ok("Company registered");
        }

        //[HttpPost("[action]")]
        //public async Task<IActionResult> ConfirmEmail(MailRequestDTO mailRequestDTO,string link)
        //{
        //	var response=await _accountService.SendConfirmationEmailAsync(mailRequestDTO.ToEmail, link);
        //	return Ok(response);
        //}

    }
}
