using JobStream.Business.DTOs.AboutUsDTO;
using JobStream.Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobStream.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutUsController : ControllerBase
    {
        private readonly IAboutUsService _aboutUsService;

        public AboutUsController(IAboutUsService aboutUsService)
        {
            _aboutUsService = aboutUsService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result=await _aboutUsService.GetAboutUsAsync();
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromForm] AboutUsPostDTO aboutUsPostDTO)
        {
             await _aboutUsService.CreateAboutUsAsync(aboutUsPostDTO);
            return Ok("About us created");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id,[FromForm] AboutUsPutDTO aboutUs)
        {
            await _aboutUsService.UpdateAboutUsAsync(id, aboutUs);
            return Ok("About us updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _aboutUsService.DeleteAboutUsAsync(id);
            return Ok("Deleted");
        }
    }
}
