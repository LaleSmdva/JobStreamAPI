using AutoMapper;
using JobStream.Business.DTOs.CategoryDTO;
using JobStream.Business.DTOs.CompanyDTO;
using JobStream.Business.DTOs.VacanciesDTO;
using JobStream.Business.Exceptions;
using JobStream.Business.HelperServices.Interfaces;
using JobStream.Business.Services.Interfaces;
using JobStream.Core.Entities;
using JobStream.DataAccess.Repositories.Implementations;
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
    private readonly ICompanyRepository _companyRepository;
    private readonly IVacanciesRepository _vacanciesRepository;


    public CategoryService(IMapper mapper, IWebHostEnvironment environment, IFileService fileService, ICategoryRepository repository, ICompanyRepository companyRepository, IVacanciesRepository vacanciesRepository)
    {
        _mapper = mapper;
        _environment = environment;
        _fileService = fileService;
        _repository = repository;
        _companyRepository = companyRepository;
        _vacanciesRepository = vacanciesRepository;
    }
    public async Task<List<CategoriesDTO>> GetAllCategories()
    {
        var categories = await _repository.GetAll()
            .Include(x => x.CategoryField)
            .Include(x => x.Vacancies)
            .Include(c => c.CompanyAndCategories)
             //.ThenInclude(cc => cc.Company) 
             .ToListAsync();
        var result = _mapper.Map<List<CategoriesDTO>>(categories);
        return result;
    }

    public async Task CreateCategoryAsync(CategoriesPostDTO categoryDto)
    {
        if (categoryDto == null)
        {
            throw new BadRequestException("Category data is missing");
        }
        var categories =  _repository.GetAll();

        if (categories.Any(c => c.Name == categoryDto.Name))
        {
            throw new AlreadyExistsException("A category with the same name already exists");
        }

        var category = new Category { Name = categoryDto.Name };

        await _repository.CreateAsync(category);
        await _repository.SaveAsync();

        //if (categoryDto.Vacancies != null && categoryDto.Vacancies.Any())
        //{
        //foreach (var vacancyDto in categoryDto.Vacancies)
        //{
        //    var result = _mapper.Map<Vacancy>(vacancyDto);
        //    result.Name = vacancyDto.Name;
        //    result.CategoryId = category.Id;
        //    result.JobType = result.JobType;
        //    result.JobSchedule = result.JobSchedule;

        //    category.Vacancies.Add(result);

        //var vacancy = new Vacancy { Name = vacancyDto.Name, CategoryId = category.Id };
        //category.Vacancies.Add(vacancy);
        //}
        //}



        //await entity.Logo.CopyFileAsync(_environment.WebRootPath, "images", "companyLogos");
        //await _fileService.CopyFileAsync(entity.Logo, _environment.WebRootPath, "images", "companyLogos");
        //var categories = _mapper.Map<Category>(entity);

        //List<Vacancy> vacancies = new List<Vacancy>();

        //foreach (var vacancy in vacancies)
        //{
        //    vacancies.Add(new Vacancy()
        //    {
        //        CategoryId = categories.Id,
        //        JobTypeId = vacancy.JobTypeId,
        //        JobScheduleId = vacancy.JobScheduleId,

        //    });

        //}
        //categories.Vacancies = vacancies;
        //await _repository.CreateAsync(categories);
        //await _repository.SaveAsync();

    }
    public async Task UpdateCategoryNameAsync(int id,CategoriesPutDTO entity)
    {
        var categories = _repository.GetByCondition(a => a.Id ==entity.Id, false);
        if (categories == null) throw new NotFoundException($"There is no article with id: {id}");
        if (id != entity.Id) throw new BadRequestException($"{entity.Id} was not found");

        var result = _mapper.Map<Category>(entity);
        _repository.Update(result);
        await _repository.SaveAsync();
    }
    public async Task DeleteCategoryAsync(int id)
    {
        var categories = _repository.GetAll().ToList();

        if (categories.All(x => x.Id != id))
        {
            throw new NotFoundException("Not Found");
        }
        var category = await _repository.GetByIdAsync(id);
        _repository.Delete(category);
        await _repository.SaveAsync();

    }
}
