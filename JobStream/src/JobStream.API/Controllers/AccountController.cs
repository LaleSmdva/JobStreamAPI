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
        public IActionResult GetAllCandidateAccounts()
        {
            try
            {
                var accounts = _accountService.GetAllCandidateAccounts();
                return Ok(accounts);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("[action]")]
        public IActionResult GetAllCompanyAccounts()
        {
            try
            {
                var accounts = _accountService.GetAllCompanyAccounts();
                return Ok(accounts);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //[HttpGet("[action]")]
        [HttpGet("candidate/{userName}")]
        public async Task<IActionResult> GetCandidateAccountByUsername(string userName)
        {
            try
            {
                var user = await _accountService.GetCandidateAccountByUsernameAsync(userName);
                return Ok(user);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RegisterCandidate(RegisterCandidateDTO candidateDto)
        {
            try
            {
                await _accountService.RegisterCandidate(candidateDto);
                return Ok("Candidate registered");
            }
            catch (DuplicateUserNameException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DuplicateEmailException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (CreateUserFailedException ex)
            {
                return BadRequest(ex.Message);
                //return StatusCode((int)HttpStatusCode.InternalServerError,ex.Message);
            }
            catch (CreateRoleFailedException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetUserRoles()
        {
            try
            {
                var list = await _accountService.GetAllRolesAsync();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpPost("[action]")]
        [ValidateRolesModel]
        public async Task<IActionResult> CreateRole(string userName, [FromQuery] List<string> roles)
        {
            try
            {
                bool isCreated = await _accountService.CreateRoleAsync(userName, roles);
                return Ok(isCreated);
            }
            catch (Business.Exceptions.ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (CreateRoleFailedException ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateRole(string userName, [FromQuery] List<string> newRoles, [FromQuery] List<string> deletedRoles)
        {
            try
            {
                bool isUpdated = await _accountService.UpdateRoleAsync(userName, newRoles, deletedRoles);
                return Ok(isUpdated);
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> RegisterCompany(RegisterCompanyDTO companyDto)
        {
            try
            {
                await _accountService.RegisterCompany(companyDto);
                return Ok("Company registered");
            }
            catch (DuplicateUserNameException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DuplicateEmailException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (CreateUserFailedException ex)
            {
                return BadRequest(ex.Message);
                //return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            catch (CreateRoleFailedException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //[HttpPost("[action]")]
        //public async Task<IActionResult> ConfirmEmail(MailRequestDTO mailRequestDTO,string link)
        //{
        //	var response=await _accountService.SendConfirmationEmailAsync(mailRequestDTO.ToEmail, link);
        //	return Ok(response);
        //}



    }
}
