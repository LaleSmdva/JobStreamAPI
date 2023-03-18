using AutoMapper;
using JobStream.Business.DTOs.CategoryDTO;
using JobStream.Business.Exceptions;
using JobStream.Business.HelperServices.Interfaces;
using JobStream.Business.Services.Interfaces;
using JobStream.Core.Entities;
using JobStream.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MimeKit.Encodings;

namespace JobStream.Business.Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;
    private readonly IVacanciesRepository _vacanciesRepository;


    public CategoryService(IMapper mapper, ICategoryRepository repository, IVacanciesRepository vacanciesRepository)
    {
        _mapper = mapper;
        _repository = repository;
        _vacanciesRepository = vacanciesRepository;
    }
    public async Task<List<CategoriesDTO>> GetAllCategories()
    {
        var categories = await _repository.GetAll()
            //.Include(x => x.CategoryField)
            //.Include(x => x.Vacancies)
            //.Include(c => c.CompanyAndCategories)
             .ToListAsync();
        if(categories.Count== 0)
        {
            throw new NotFoundException("No category found");
        }
        var result = _mapper.Map<List<CategoriesDTO>>(categories);
        return result;
    }

    public async Task<CategoriesDTO> GetCategoryAsync(int id)
    {
        var category = await _repository.GetAll()
            .Include(x => x.CategoryField)
            .Include(x => x.Vacancies)
            .Include(c => c.CompanyAndCategories).FirstOrDefaultAsync(c=>c.Id==id);
        if(category is null)
        {
            throw new NotFoundException("Not found");
        }

        var result = _mapper.Map<CategoriesDTO>(category);
       
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
        var category=await _repository.GetByIdAsync(id);
        if(category is null)
        {
            throw new NotFoundException("Category not found");
        }
        var vacancies=_vacanciesRepository.GetByCondition(c => c.CategoryId == id).ToList();
        foreach (var vacancy in vacancies)
        {
            if(vacancy.CategoryId==id)
            {
                throw new BadRequestException("Category contains vacancies");
            }
        }
        _repository.Delete(category);
        await _repository.SaveAsync();

    }
}
