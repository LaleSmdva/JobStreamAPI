using AutoMapper;
using JobStream.Business.DTOs.CategoryDTO;
using JobStream.Business.DTOs.CompanyDTO;
using JobStream.Business.HelperServices.Interfaces;
using JobStream.Business.Services.Interfaces;
using JobStream.Core.Entities;
using JobStream.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _environment;
    private readonly IFileService _fileService;

    public CategoryService(IMapper mapper, IWebHostEnvironment environment, IFileService fileService, ICategoryRepository repository)
    {
        _mapper = mapper;
        _environment = environment;
        _fileService = fileService;
        _repository = repository;
    }
    public List<CategoriesDTO> GetAll()
    {
        var categories = _repository.GetAll().Include(x=>x.CategoryField).ToList();
        var result = _mapper.Map<List<CategoriesDTO>>(categories);
        return result;
    }

    public async Task CreateAsync(CategoriesPostDTO entity)
    {
        //await entity.Logo.CopyFileAsync(_environment.WebRootPath, "images", "companyLogos");
        //await _fileService.CopyFileAsync(entity.Logo, _environment.WebRootPath, "images", "companyLogos");
        var categories = _mapper.Map<Category>(entity);

        List<Vacancy> vacancies = new List<Vacancy>();
        foreach (var vacancy in entity.Vacancies)
        {
            vacancies.Add(new Vacancy()
            {
                CategoryId= vacancy.CategoryId,
                JobTypeId= vacancy.JobTypeId,
                JobScheduleId= vacancy.JobScheduleId,
            });
        }
        await _repository.CreateAsync(categories);
        await _repository.SaveAsync();

    }
}
