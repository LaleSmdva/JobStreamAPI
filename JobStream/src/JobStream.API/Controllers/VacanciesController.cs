﻿using JobStream.Business.DTOs.VacanciesDTO;
using JobStream.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobStream.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacanciesController : ControllerBase
    {
        private readonly IVacanciesService _vacanciesService;

        public VacanciesController(IVacanciesService vacanciesService)
        {
            _vacanciesService = vacanciesService;
        }

        [HttpGet]
        public IActionResult GetAllVacancies()
        {
            var vacancies = _vacanciesService.GetAll();
            return Ok(vacancies);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVacancyByIdAsync(int id)
        {
            var vacancies = await _vacanciesService.GetVacancyByIdAsync(id);
            return Ok(vacancies);
        }
        [HttpPost("GetVacanciesByCategoryIDs")]
        public async Task<IActionResult> GetVacanciesByCategoryAsync(List<int> categoryIds)
        {
            var vacancies = await _vacanciesService.GetVacanciesByCategoryAsync(categoryIds);
            return Ok(vacancies);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> SearchVacancies([FromQuery] string? keyword, [FromQuery] string? location, [FromQuery] List<int>? categoryId, [FromQuery] string? companyName)
        {
            List<VacanciesDTO> list = await _vacanciesService.SearchVacancies(keyword, location, categoryId, companyName);
            return Ok(list);
        }

    }
}
