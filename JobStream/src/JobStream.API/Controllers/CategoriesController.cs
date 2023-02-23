using JobStream.Business.DTOs.CategoryDTO;
using JobStream.Business.DTOs.CompanyDTO;
using JobStream.Business.Exceptions;
using JobStream.Business.Services.Implementations;
using JobStream.Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace JobStream.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllCompanies()
        {
            try
            {
                var companies =await _categoryService.GetAll();
                return Ok(companies);
            }
            catch (NotFoundException)
            {
                return NotFound("Not Found");
            }
            //catch (Exception)
            //{
            //	throw new InvalidOperationException("The requested resource could not be found.");	

            //}
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddCompany([FromForm] CategoriesPostDTO category)
        {

            try
            {
                //var existingCompany = await _companyService.GetByIdAsync(company.Id);
               
                var categories = await _categoryService.GetAll();
                if (categories.Any(c => c.Name == category.Name))
                {
                    throw new BadRequestException("A company with the same name and email already exists");
                }

                await _categoryService.CreateAsync(category);
                return Ok("Successfully created");
            }
        
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);

            }
        }
    }
}
