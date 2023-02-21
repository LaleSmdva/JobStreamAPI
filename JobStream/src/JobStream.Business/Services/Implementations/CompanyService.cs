using AutoMapper;
using JobStream.Business.DTOs.CompanyDTO;
using JobStream.Business.Exceptions;
using JobStream.Business.HelperServices.Interfaces;
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

        public CompanyService(ICompanyRepository repository, IMapper mapper, IWebHostEnvironment environment, IFileService fileService, IVacanciesRepository vacanciesRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _environment = environment;
            _fileService = fileService;
            _vacanciesRepository = vacanciesRepository;
        }

        public  List<CompanyDTO> GetAllAsync()
		{
			var companies = _repository.GetAll().ToList();
			var result = _mapper.Map<List<CompanyDTO>>(companies);
			return result;
		}

		public async Task CreateAsync(CompanyPostDTO entity)
		{
			//await entity.Logo.CopyFileAsync(_environment.WebRootPath, "images", "companyLogos");
			await _fileService.CopyFileAsync(entity.Logo, _environment.WebRootPath, "images", "companyLogos");
			var companies = _mapper.Map<Company>(entity);
			///////////////////////////////////
            //List<Vacancy> vacancies = new();
            //foreach (var topicId in entity.Vacancies)
            //{
            //    vacancies.Add(new()
            //    {
            //        CompanyId=companies.Id,
            //        JobTypeId=companies.Id
            //    });
            //}
            //companies.Vacancies = vacancies;

			//////////////////////////////

            await _repository.CreateAsync(companies);
			await _repository.SaveAsync();
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
