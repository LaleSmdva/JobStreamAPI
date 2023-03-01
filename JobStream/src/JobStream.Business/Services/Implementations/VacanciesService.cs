using AutoMapper;
using AutoMapper.Internal;
using Hangfire;
using JobStream.Business.DTOs.CompanyDTO;
using JobStream.Business.DTOs.VacanciesDTO;
using JobStream.Business.Exceptions;
using JobStream.Business.Services.Interfaces;
using JobStream.Business.Validators.Vacancies;
using JobStream.Core.Entities;
using JobStream.DataAccess.Repositories.Implementations;
using JobStream.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
            var vacancies = _vacanciesRepository.GetAll()
                .Where(v => v.isDeleted == false).ToList();
            var result = _mapper.Map<List<VacanciesDTO>>(vacancies);
            return result;
        }

        public async Task<List<VacanciesDTO>> GetVacanciesByCategoryAsync(List<int> categoryIds)
        {
            //List<Vacancy> vacancy = await _vacanciesRepository.GetByIdAsync(categoryId).Where(v => v.isDeleted == false).ToList();
            List<Vacancy> vacanciesDTOs= new List<Vacancy>(); 
            foreach (var categoryId in categoryIds)
            {

                var vacancy = await _vacanciesRepository.GetByIdAsync(categoryId);
                vacanciesDTOs.Add(vacancy);
       
            }
            var list=_mapper.Map<List<VacanciesDTO>>(vacanciesDTOs);
            return list;

        }

        public async Task<VacanciesDTO> GetVacancyByIdAsync(int id)
        {
            var vacancy = await _vacanciesRepository.GetByIdAsync(id);
            var result = _mapper.Map<VacanciesDTO>(vacancy);
            return result;
        }
        
        public IEnumerable<Vacancy> GetExpiredVacancies()
        {
            var currentDateUtc = DateTime.Now;
            return _vacanciesRepository.GetAll().Where(v => v.ClosingDate <= currentDateUtc);
        }


        //[AutomaticRetry(Attempts = 0)]
        public async Task VacancyCleanUp()
        {
            var expiredVacancies = GetExpiredVacancies();
            foreach (var vacancy in expiredVacancies)
            {
                _vacanciesRepository.Delete(vacancy);
            }
            await _vacanciesRepository.SaveAsync();
        }


        public async Task<List<VacanciesDTO>> SearchVacancies(string? keyword, string? location, List<int>? categoryId, string? companyName)
        {
            List<Vacancy> vacancies = _vacanciesRepository.GetAll()
             .Where(v => v.Name.Contains(keyword) || v.Location.Contains(location)
                 || categoryId.Contains(v.CategoryId) || v.Company.Name.Contains(companyName))
             .ToList();
            var result = _mapper.Map<List<VacanciesDTO>>(vacancies);
            return result;


        }






    }
}
