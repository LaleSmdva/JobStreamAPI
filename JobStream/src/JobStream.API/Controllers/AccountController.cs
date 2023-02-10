using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobStream.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		[HttpPost("register/candidate")]
		public async Task<IActionResult> RegisterCandidate(/*RegisterCandidateDto candidateDto*/)
		{
			//AppUser user = new()
			//{
			//	Fullname = candidateDto.FullName,
			//	Email = candidateDto.Email,
			//};
			//var identityResult = await _userManager.CreateAsync(user, candidateDto.Password);
			return Ok("Candidate registered");
		}

		[HttpPost("register/company")]
		public async Task<IActionResult> RegisterCompany(/*RegisterCompanyDto companyDto*/)
		{
			return Ok("Company registered");
		}
	}
}
