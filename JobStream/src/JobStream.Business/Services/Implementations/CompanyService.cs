using AutoMapper;
using JobStream.Business.DTOs.CompanyDTO;
using JobStream.Business.Exceptions;
using JobStream.Business.HelperServices.Interfaces;
using JobStream.Business.Mappers;
using JobStream.Business.Services.Interfaces;
using JobStream.Business.Utilities;
using JobStream.Core.Entities;
using JobStream.DataAccess.Contexts;
using JobStream.DataAccess.Repositories.Implementations;
using JobStream.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace JobStream.Business.Services.Implementations
{
	public class CompanyService : ICompanyService
	{
		private readonly ICompanyRepository _repository;
		private readonly IMapper _mapper;
		private readonly IWebHostEnvironment _environment;
		private readonly IFileService _fileService;
		private readonly IVacanciesRepository _vacanciesRepository;
		private readonly ICategoryRepository _categoryRepository;
		private readonly ICompanyAndCategoryRepository _companyAndCategoryRepository;

        public CompanyService(ICompanyRepository repository, IMapper mapper, IWebHostEnvironment environment, IFileService fileService, IVacanciesRepository vacanciesRepository, ICategoryRepository categoryRepository, ICompanyAndCategoryRepository companyAndCategoryRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _environment = environment;
            _fileService = fileService;
            _vacanciesRepository = vacanciesRepository;
            _categoryRepository = categoryRepository;
            _companyAndCategoryRepository = companyAndCategoryRepository;
        }


        ///AddCategory
        ///categoryname,
        ///AddVacancy
        ///ContactApplicant



        public async Task<List<CompanyDTO>> GetAllAsync()
		{
			var companies =await _repository.GetAll().Include(v => v.Vacancies)
				.Include(x=>x.CompanyAndCategories).ThenInclude(cc=>cc.Category).ToListAsync();
			var result = _mapper.Map<List<CompanyDTO>>(companies);
			return result;
		}

        ///one to  many? many to  many
        //      public async Task AddCategory(int companyId,int categoryId)
        //{
        //	var company=await _repository.GetByIdAsync(companyId);
        //	var category=await _categoryRepository.GetByIdAsync(categoryId);

        //	company.CompanyAndCategories.Add();
        //}


	
		/// one to many
		
   //     public async Task AddVacancy(int vacancyId, int companyId)
   //     {
   //         var company = await _repository.GetByIdAsync(companyId);
   //         var vacancy = await _vacanciesRepository.GetByIdAsync(vacancyId);

			//company.Vacancies.Add(vacancy);
   //     }

        public async Task CreateAsync(CompanyPostDTO entity)
		{
			//await entity.Logo.CopyFileAsync(_environment.WebRootPath, "images", "companyLogos");
			await _fileService.CopyFileAsync(entity.Logo, _environment.WebRootPath, "images", "companyLogos");
			var companies = _mapper.Map<Company>(entity);

			if (entity.CatagoriesId == null) throw new NullReferenceException("Category can not be null");
			foreach (var catId in entity.CatagoriesId)
			{
				var category=await _categoryRepository.GetByIdAsync(catId);
				if (category == null) throw new NotFoundException("Not found");


			}
			await _repository.CreateAsync(companies);
			await _repository.SaveAsync();

			foreach (var catID in entity.CatagoriesId)
			{
				CompanyAndCategory companyAndCategory = new();
				companyAndCategory.CategoryId = catID;
				companyAndCategory.CompanyId =companies.Id;
				await _companyAndCategoryRepository.CreateAsync(companyAndCategory);

            }
            await _companyAndCategoryRepository.SaveAsync();


            //_companyAndCategoryRepository.CreateAsync(companyAndCatagories.)


            ///////////////////////////////////
            //List<Vacancy> vacancies = new();
            //foreach (var topicId in entity.Vacancies)
            //{
            //	vacancies.Add(new()
            //	{
            //		CompanyId = companies.Id,
            //		JobTypeId = companies.Id
            //	});
            //}
            //companies.Vacancies = vacancies;

            //////////////////////////////
            ///
            //List<CompanyAndCategory> companyAndCategories= new();
            //foreach (var item in entity.CompanyAndCategories)
            //{
            //	companyAndCategories.Add(new()
            //	{
            //		CompanyId = companies.Id,
            //		CategoryId=companies.Id
            //	});
            //}

            //companies.CompanyAndCategories= companyAndCategories;

        }

		public async Task Delete(int id)
		{
			var companies = _repository.GetAll();
			if (companies.All(x => x.Id != id))
			{
				throw new NotFoundException("Not Found");
			}
			var company = await _repository.GetByIdAsync(id);
			var result = _mapper.Map<Company>(company);
			_repository.Delete(result);
			await _repository.SaveAsync();
		}



		public List<CompanyDTO> GetByCondition(Expression<Func<Company, bool>> expression)
		{

			var company = _repository.GetByCondition(expression).ToList();
			var result = _mapper.Map<List<CompanyDTO>>(company);
			return result;
		}

		public async Task<CompanyDTO> GetByIdAsync(int id)
		{
			var companies = _repository.GetAll();
			if (companies.All(x => x.Id != id))
			{
				throw new NotFoundException("Not Found");
			}
			var company = await _repository.GetByIdAsync(id);
			var result = _mapper.Map<CompanyDTO>(company);
			return result;
		}


		public async Task Update(int id, CompanyPutDTO company)
		{
			var Company = _repository.GetByCondition(c => c.Id == company.Id, false);
			if (Company == null) throw new NotFoundException("Not Found");
			if (id != company.Id) throw new BadRequestException("Id is not valid");
			var result = _mapper.Map<Company>(company);
			_repository.Update(result);
			await _repository.SaveAsync();
		}
	}
}
