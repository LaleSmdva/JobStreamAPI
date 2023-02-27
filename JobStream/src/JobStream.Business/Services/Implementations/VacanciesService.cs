using AutoMapper;
using Hangfire;
using JobStream.Business.DTOs.CompanyDTO;
using JobStream.Business.DTOs.VacanciesDTO;
using JobStream.Business.Exceptions;
using JobStream.Business.Services.Interfaces;
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


        ///



        public async Task<string> ApplyVacancy(VacanciesDTO vacancy, CandidateResume candidateResume)
        {
            if (vacancy == null) throw new NotFoundException("Vacancy not found");
            if (candidateResume == null) throw new NotFoundException("Resume not found");
            //if (vacancyId <= 0) throw new BadRequestException("Invalid vacancy ID.");

            var vac = await _vacanciesRepository.GetByIdAsync(vacancy.Id);

            var application = new CandidateResume()
            {
                CV = candidateResume.CV
            };

            //_vacanciesRepository.CreateAsync(application);

            return "Ok";
        }
        public List<VacanciesDTO> GetAll()
        {
            var vacancies = _vacanciesRepository.GetAll()
                .Where(v => v.isDeleted == false).ToList();
            var result = _mapper.Map<List<VacanciesDTO>>(vacancies);
            return result;
        }

        public List<VacanciesDTO> GetVacanciesByCategory(Expression<Func<Vacancy, bool>> expression)
        {
            List<Vacancy> vacancy = _vacanciesRepository.GetByCondition(expression).Where(v => v.isDeleted == false).ToList();
            if (vacancy is null) throw new NotFoundException("No vacancy was found");
            var result = _mapper.Map<List<VacanciesDTO>>(vacancy);
            return result;
        }

        public async Task<VacanciesDTO> GetVacancyByIdAsync(int id)
        {
            var vacancy = await _vacanciesRepository.GetByIdAsync(id);
            var result = _mapper.Map<VacanciesDTO>(vacancy);
            return result;
        }
        public async Task CreateVacancyAsync(VacanciesPostDTO entity)
        {
            var company = await _companyRepository.GetByIdAsync(entity.CompanyId);
            var category = await _categoryRepository.GetByIdAsync(entity.CategoryId);
            var jobType = await _jobTypeRepository.GetByIdAsync(entity.JobTypeId);
            var jobSchedule = await _jobScheduleRepository.GetByIdAsync(entity.JobScheduleId);

            var vacancy = _mapper.Map<Vacancy>(entity);

            vacancy.Company = company;
            vacancy.Category = category;
            vacancy.JobType = jobType;
            vacancy.JobSchedule = jobSchedule;
            vacancy.PostedOn = DateTime.Now;
            vacancy.ClosingDate = DateTime.Now.AddDays(30);


            await _vacanciesRepository.CreateAsync(vacancy);
            await _vacanciesRepository.SaveAsync();
        }

        public async Task UpdateVacancyAsync(int id, VacanciesPutDTO vacancy)
        {
            // Vacancy ozu,JobTypeId,JobScheduleID,CategoryId,CompanyId
            var vacancies = _vacanciesRepository.GetByCondition(c => c.Id == vacancy.Id, false);
            if (vacancies == null) throw new NotFoundException("Not Found");
            if (id != vacancy.Id) throw new BadRequestException("Id is not valid");
            var result = _mapper.Map<Vacancy>(vacancy);


            // Get all the properties of the VacanciesPutDTO class
            var properties = typeof(VacanciesPutDTO).GetProperties();

            //foreach (var property in properties)
            //{
            //    // Get the value of the property in the VacanciesPutDTO object
            //    var value = property.GetValue(vacancy);

            //    // Check if the value is null or empty
            //    //if (value != null && !string.IsNullOrEmpty(value.ToString()))
            //    //{
            //        // Get the corresponding property in the Vacancy object
            //        var vacancyProperty = typeof(Vacancy).GetProperty(property.Name);

            //        // Set the value of the Vacancy property to the value of the VacanciesPutDTO property
            //        vacancyProperty.SetValue(result, value);
            //    //}
            //}


            //result.JobTypeId= vacancy.JobTypeId;
            //result.JobScheduleId = vacancy.JobScheduleId;
            //result.CategoryId= vacancy.CategoryId;  
            //result.CompanyId= vacancy.CompanyId;
            //
            //result.ClosingDate= DateTime.UtcNow.AddDays(vacancy.ClosingDate);
            _vacanciesRepository.Update(result);
            await _vacanciesRepository.SaveAsync();
        }


        public async Task DeleteVacancyAsync(int id)
        {
            var vacancy = await _vacanciesRepository.GetByIdAsync(id);
            var result = _mapper.Map<Vacancy>(vacancy);
            _vacanciesRepository.Delete(result);
            await _vacanciesRepository.SaveAsync();
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








    }
}
