using AutoMapper;
using JobStream.Business.DTOs.CompanyDTO;
using JobStream.Business.DTOs.VacanciesDTO;
using JobStream.Business.Exceptions;
using JobStream.Business.Services.Interfaces;
using JobStream.Core.Entities;
using JobStream.DataAccess.Repositories.Implementations;
using JobStream.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Services.Implementations
{
	public class VacanciesService : IVacanciesService
	{
		private readonly IVacanciesRepository _vacanciesRepository;
		private readonly IMapper _mapper;
		private readonly ICompanyRepository _companyRepository;
		private readonly ICategoryRepository _categoryRepository;
		private readonly IJobTypeRepository _jobTypeRepository;
		private readonly IJobScheduleRepository _jobScheduleRepository;

        public VacanciesService(IMapper mapper, IVacanciesRepository vacanciesRepository,
			ICompanyRepository companyRepository, ICategoryRepository categoryRepository,
			IJobTypeRepository jobTypeRepository, IJobScheduleRepository jobScheduleRepository)
        {

            _mapper = mapper;
            _vacanciesRepository = vacanciesRepository;
            _companyRepository = companyRepository;
            _categoryRepository = categoryRepository;
            _jobTypeRepository = jobTypeRepository;
            _jobScheduleRepository = jobScheduleRepository;
        }

        public List<VacanciesDTO> GetAll()
		{
			var vacancies = _vacanciesRepository.GetAll().ToList();
			var result = _mapper.Map<List<VacanciesDTO>>(vacancies);
			return result;
		}

		public List<VacanciesDTO> GetByCondition(Expression<Func<Vacancy, bool>> expression)
		{
			var vacancy = _vacanciesRepository.GetByCondition(expression).ToList();
			var result = _mapper.Map<List<VacanciesDTO>>(vacancy);
			return result;
		}

		public async Task<VacanciesDTO> GetByIdAsync(int id)
		{
			var vacancy = await _vacanciesRepository.GetByIdAsync(id);
			var result = _mapper.Map<VacanciesDTO>(vacancy);
			return result;
		}
		public async Task CreateAsync(VacanciesPostDTO entity)
		{
			var vacancy = _mapper.Map<Vacancy>(entity);
			//vacancy.Company = company;
			var company=await _companyRepository.GetByIdAsync(entity.CompanyId);
			var category=await _categoryRepository.GetByIdAsync(entity.CategoryId);
			var jobType=await _jobTypeRepository.GetByIdAsync(entity.JobTypeId);
			var jobSchedule=await _jobScheduleRepository.GetByIdAsync(entity.JobScheduleId);

			vacancy.Company = company;
			vacancy.Category = category;
			vacancy.JobType = jobType;
			vacancy.JobSchedule= jobSchedule;
			vacancy.PostedOn = DateTime.UtcNow;
			vacancy.ClosingDate = DateTime.UtcNow.AddDays(30);

			await _vacanciesRepository.CreateAsync(vacancy);
			await _vacanciesRepository.SaveAsync();
			 //_mapper.Map<VacanciesPostDTO>(entity);
		}

		public async Task Delete(int id)
		{
			var vacancy = await _vacanciesRepository.GetByIdAsync(id);
			var result = _mapper.Map<Vacancy>(vacancy);
			_vacanciesRepository.Delete(result);
			await _vacanciesRepository.SaveAsync();
		}

	

		public async Task Update(int id, VacanciesPutDTO vacancy)
		{
			var vacancies = _vacanciesRepository.GetByCondition(c => c.Id == vacancy.Id, false);
			if (vacancies == null) throw new NotFoundException("Not Found");
			if (id != vacancy.Id) throw new BadRequestException("Id is not valid");
			var result = _mapper.Map<Vacancy>(vacancy);
			_vacanciesRepository.Update(result);
			await _vacanciesRepository.SaveAsync();
		}


		
	}
}
