﻿using AutoMapper;
using JobStream.Business.DTOs.CompanyDTO;
using JobStream.Business.DTOs.InvitationDTO;
using JobStream.Business.DTOs.VacanciesDTO;
using JobStream.Business.Exceptions;
using JobStream.Business.HelperServices.Interfaces;
using JobStream.Business.Mappers;
using JobStream.Business.Services.Interfaces;
using JobStream.Business.Utilities;
using JobStream.Core.Entities;
using JobStream.Core.Entities.Identity;
using JobStream.Core.Interfaces;
using JobStream.DataAccess.Contexts;
using JobStream.DataAccess.Repositories.Implementations;
using JobStream.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Org.BouncyCastle.Math.EC.Rfc7748;
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
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IJobTypeRepository _jobTypeRepository;
        private readonly IJobScheduleRepository _jobScheduleRepository;
        private readonly ICandidateResumeRepository _candidateResumeRepository;
        private readonly IMailService _mailService;
        private readonly IApplicationRepository _applicationRepository;


        public CompanyService(ICompanyRepository repository, IMapper mapper, IWebHostEnvironment environment, IFileService fileService, IVacanciesRepository vacanciesRepository, ICategoryRepository categoryRepository, ICompanyAndCategoryRepository companyAndCategoryRepository, AppDbContext context, UserManager<AppUser> userManager, IJobTypeRepository jobTypeRepository, IJobScheduleRepository jobScheduleRepository, ICandidateResumeRepository candidateResumeRepository, IMailService mailService, IApplicationRepository applicationRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _environment = environment;
            _fileService = fileService;
            _vacanciesRepository = vacanciesRepository;
            _categoryRepository = categoryRepository;
            _companyAndCategoryRepository = companyAndCategoryRepository;
            _context = context;
            _userManager = userManager;
            _jobTypeRepository = jobTypeRepository;
            _jobScheduleRepository = jobScheduleRepository;
            _candidateResumeRepository = candidateResumeRepository;
            _mailService = mailService;
            _applicationRepository = applicationRepository;
        }


        public async Task<List<CompanyDTO>> GetAllAsync()
        {
            var companies = await _repository.GetAll().Include(v => v.Vacancies)
                .Include(x => x.CompanyAndCategories).Where(x => x.IsDeleted != true)
                 .ToListAsync();
            var result = _mapper.Map<List<CompanyDTO>>(companies);
            return result;
        }


        public List<CompanyDTO> GetCompaniesByName(string companyName)
        {

            var company = _repository.GetAll().Where(n => n.Name.Contains(companyName))
                .Where(b => b.IsDeleted != true).ToList();
            var result = _mapper.Map<List<CompanyDTO>>(company);
            return result;
        }

        public async Task<CompanyDTO> GetByIdAsync(int id)
        {
            var companies = await _repository.GetAll()
                .Include(v => v.Vacancies)
                .Include(x => x.CompanyAndCategories)
                .Where(b => b.IsDeleted != true).ToListAsync();
            if (companies.All(x => x.Id != id))
            {
                throw new NotFoundException("Not Found");
            }
            var company = await _repository.GetByIdAsync(id);
            var result = _mapper.Map<CompanyDTO>(company);
            return result;
        }

        //public async Task CreateAsync(CompanyPostDTO entity)
        //{
        //    var alreadyExists = _repository.GetAll().Any(n => n.Name == entity.Name);
        //    if (alreadyExists) throw new AlreadyExistsException("Company with that name already exists");

        //    if (entity.CatagoriesId == null) throw new NullReferenceException("Category can not be null");
        //    foreach (var catId in entity.CatagoriesId)
        //    {
        //        var category = await _categoryRepository.GetByIdAsync(catId);
        //        if (category == null) throw new NotFoundException("No category found with that id");
        //    }
        //    if (await _userManager.Users.AllAsync(e => e.Email == entity.Email))
        //        throw new AlreadyExistsException("There exists user with that email");

        //    var User = await _userManager.Users.SingleOrDefaultAsync(u => u.Email == entity.Email);
        //    if (User is null) throw new NotFoundException($"User with email:{entity.Email} not found");



        //    var fileName = await _fileService.CopyFileAsync(entity.Logo, _environment.WebRootPath, "images", "companyLogos");


        //    var company = _mapper.Map<Company>(entity);
        //    company.Logo = fileName;
        //    company.UserId = User.Id;


        //    _repository.Update(company);
        //    await _repository.SaveAsync();


        //    List<CompanyAndCategory> companyAndCategoryList = new();
        //    foreach (var catID in entity.CatagoriesId)
        //    {
        //        if (companyAndCategoryList.Any(i => i.CategoryId == catID))
        //            throw new AlreadyExistsException($"Category id {catID}  already exists in the company");
        //        CompanyAndCategory companyAndCategory = new();
        //        companyAndCategory.CategoryId = catID;
        //        companyAndCategory.CompanyId = company.Id;
        //        companyAndCategoryList.Add(companyAndCategory);

        //    }
        //    foreach (var item in companyAndCategoryList)
        //    {
        //        await _companyAndCategoryRepository.CreateAsync(item);
        //    }
        //    await _companyAndCategoryRepository.SaveAsync();
        //}
        //AYRI  


        //List<Vacancy> vacancies = new();
        //foreach (var topicId in entity.Vacancies)
        //{
        //    vacancies.Add(new()
        //    {
        //        CompanyId = companies.Id,
        //        JobTypeId = companies.Id
        //    });
        //}
        //companies.Vacancies = vacancies;

        //_companyAndCategoryRepository.CreateAsync(companyAndCatagories.)


        ///////////////////////////////////
        //List<Vacancy> vacancies = new();
        //foreach (var topicId in entity.Vacancies)
        //{
        //    vacancies.Add(new()
        //    {
        //        CompanyId = companies.Id,
        //        JobTypeId = companies.Id
        //    });
        //}
        //companies.Vacancies = vacancies;


        public async Task UpdateCompanyAccount(string id, List<int> addedCategoryId, List<int> deletedCategoryId, CompanyPutDTO companyPutDTO)
        {
            /// Updating Company

            Company company = await _repository.GetAll().FirstOrDefaultAsync(x => x.UserId == id);

            if (company is null || company.IsDeleted == true) throw new NotFoundException("Company not found");


            if (!companyPutDTO.Logo.CheckFileFormat("image/"))
            {
                throw new FileFormatException("Choose an image type");
            }
            //var companies = _repository.GetByCondition(a => a.Id == companyPutDTO.Id, false);
            if (await _repository.GetAll().AllAsync(x => x.UserId == id))
                throw new BadRequestException("Id's do not match");
            //if (companies == null) throw new NotFoundException($"There is no company with id: {id}");
            //if (id != companyPutDTO.Id) throw new BadRequestException($"{companyPutDTO.Id} was not found");


            var user = await _userManager.FindByIdAsync(id);
            if (user.Email != companyPutDTO.Email)
                throw new BadRequestException("Can not change your email address");


            var fileName = await _fileService.CopyFileAsync(companyPutDTO.Logo, _environment.WebRootPath, "images", "companyLogos");

            company.Name = companyPutDTO.Name;
            company.AboutCompany = companyPutDTO.AboutCompany;
            company.Telephone = companyPutDTO.Telephone;
            company.Logo = fileName;
            company.AppUser = user;
            company.UserId = id;
            company.EmailForCv = companyPutDTO.EmailForCv;
            company.NumberOfEmployees = companyPutDTO.NumberOfEmployees;
            company.Location = companyPutDTO.Location;
            company.Email = companyPutDTO.Email;


            _repository.Update(company);
            await _repository.SaveAsync();

            // Add Category
            List<CompanyAndCategory> companyAndCategoryList = new();
            foreach (var catID in addedCategoryId)
            {
                CompanyAndCategory companyAndCategory = new();
                companyAndCategory.CategoryId = catID;
                companyAndCategory.CompanyId = company.Id;
                companyAndCategoryList.Add(companyAndCategory);

            }
            foreach (var item in companyAndCategoryList)
            {
                await _companyAndCategoryRepository.CreateAsync(item);
            }
            await _companyAndCategoryRepository.SaveAsync();


            // Delete Category
            foreach (var categoryId in deletedCategoryId)
            {
                //Throws exception if category contains vacancies
                if (await _categoryRepository.GetByCondition(x => x.Id == categoryId).AllAsync(x => x.Vacancies.Count != 0))
                {
                    throw new BadRequestException("You are trying to delete category that contains vacancies");
                }
                var companyAndCategory = await _companyAndCategoryRepository
                    .GetByCondition(cac => cac.CategoryId == categoryId && cac.CompanyId == company.Id, true)
                    .FirstOrDefaultAsync();

                if (companyAndCategory != null)
                {
                    _companyAndCategoryRepository.Delete(companyAndCategory);
                }
            }
            await _companyAndCategoryRepository.SaveAsync();


        }
        //public async Task Update(int id, List<int> addedCategoryId, List<int> deletedCategoryId, CompanyPutDTO companyPutDTO)
        //{
        //    /// Updating Company
        //    Company company = await _repository.GetByIdAsync(id);
        //    if (company is null || company.IsDeleted == true) throw new NotFoundException("Company not found");
        //    if (!companyPutDTO.Logo.CheckFileFormat("image/"))
        //    {
        //        throw new FileFormatException("Choose an image type");
        //    }
        //    var companies = _repository.GetByCondition(a => a.Id == companyPutDTO.Id, false);
        //    if (companies == null) throw new NotFoundException($"There is no company with id: {id}");
        //    if (id != companyPutDTO.Id) throw new BadRequestException($"{companyPutDTO.Id} was not found");


        //    var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == company.UserId);
        //    if (user.Email != companyPutDTO.Email)
        //        throw new BadRequestException("Can not change your email address");


        //    var fileName = await _fileService.CopyFileAsync(companyPutDTO.Logo, _environment.WebRootPath, "images", "companyLogos");

        //    company.Name = companyPutDTO.Name;
        //    company.AboutCompany = companyPutDTO.AboutCompany;
        //    company.Email = companyPutDTO.Email;
        //    company.Telephone = companyPutDTO.Telephone;
        //    company.Logo = fileName;
        //    company.AppUser = user;
        //    company.UserId = user.Id;
        //    company.EmailForCv = companyPutDTO.EmailForCv;
        //    company.NumberOfEmployees = companyPutDTO.NumberOfEmployees;
        //    company.IncorporationDate = companyPutDTO.IncorporationDate;

        //    _repository.Update(company);
        //    await _repository.SaveAsync();

        //    // Add Category
        //    List<CompanyAndCategory> companyAndCategoryList = new();
        //    foreach (var catID in addedCategoryId)
        //    {
        //        CompanyAndCategory companyAndCategory = new();
        //        companyAndCategory.CategoryId = catID;
        //        companyAndCategory.CompanyId = company.Id;
        //        companyAndCategoryList.Add(companyAndCategory);

        //    }
        //    foreach (var item in companyAndCategoryList)
        //    {
        //        await _companyAndCategoryRepository.CreateAsync(item);
        //    }
        //    await _companyAndCategoryRepository.SaveAsync();


        //    // Delete Category
        //    foreach (var categoryId in deletedCategoryId)
        //    {
        //        var companyAndCategory = await _companyAndCategoryRepository
        //            .GetByCondition(cac => cac.CategoryId == categoryId && cac.CompanyId == company.Id, true)
        //            .FirstOrDefaultAsync();

        //        if (companyAndCategory != null)
        //        {
        //            _companyAndCategoryRepository.Delete(companyAndCategory);
        //        }
        //    }
        //    await _companyAndCategoryRepository.SaveAsync();


        //}
        //Ayri

        ///Updating Vacancy for that Company
        //    var company = _repository.GetAll().Include(c => c.Vacancies)
        //.FirstOrDefault(c => c.Id == id);

        //    //    if (company == null)
        //    //    {
        //    //        throw new NotFoundException("");
        //    //    }

        //    var vacancy = company.Vacancies.FirstOrDefault(v => v.Id == companyPutDTO.vacancyId);

        //    if (vacancy == null)
        //    {
        //        throw new NotFoundException("");
        //    }

        //    //vacancy.CompanyId = id;
        //    var ress = _mapper.Map<CompanyPutDTO>(company);
        //    ress.vacancyId = vacancyId;
        //    await _context.SaveChangesAsync();


        //company.vacancyId = vacancyId;
        //var resultt = _mapper.Map<Company>(company);
        //_context.Update(resultt);
        //await _context.SaveChangesAsync();



        ///////////////////////////////////////

        //var res=_mapper.Map<Company>(company);
        //var vacancy = await _repository.GetByIdAsync(res.Id);
        //if (vacancy == null) throw new NotFoundException("Not found");
        //else
        //{
        //    var res = _mapper.Map<Company>(vacId);
        //    _repository.Update(res);
        //}

        //List<Company> companies = _repository.GetAll().ToList();
        //var list = _mapper.Map<List<CompanyPutDTO>>(companies);
        //if (vacancyId != Idd) continue;
        //}
        //

        //
        //var result = _mapper.Map<Company>(company);
        //_repository.Update(result);

        //await _repository.SaveAsync();


        public async Task DeleteCompany(int id)
        {
            var companies = _repository.GetAll();
            if (companies.All(x => x.Id != id))
            {
                throw new NotFoundException("Not Found");
            }
            Company company = await _repository.GetByIdAsync(id);
            AppUser user = await _userManager.FindByEmailAsync(company.Email);
            var result = _mapper.Map<Company>(company);
            result.IsDeleted = true;
            //user.IsDeleted = true;

            //_repository.Delete(result);
            await _repository.SaveAsync();
        }

        public async Task AddVacancyToCompany(int id, VacanciesPostDTO vacanciesPostDTO)
        {

            var company = await _repository.GetAll().FirstOrDefaultAsync(c => c.Id == id);
            if (company == null)
            {
                throw new NotFoundException("No company found");
            }
            var isExists = await _vacanciesRepository.GetAll().Where(x => x.Name == vacanciesPostDTO.Name && x.CompanyId == vacanciesPostDTO.CompanyId).FirstOrDefaultAsync();
            if(isExists != null) throw new AlreadyExistsException("Vacancy with same name already exists");

            var category = await _categoryRepository.GetByIdAsync(vacanciesPostDTO.CategoryId);
            var jobType = await _jobTypeRepository.GetByIdAsync(vacanciesPostDTO.JobTypeId);
            var jobSchedule = await _jobScheduleRepository.GetByIdAsync(vacanciesPostDTO.JobScheduleId);


            var vacancy = _mapper.Map<Vacancy>(vacanciesPostDTO);
            vacancy.Company = company;
            vacancy.JobSchedule = jobSchedule;
            vacancy.JobType = jobType;
            vacancy.Category = category;
            vacancy.PostedOn = DateTime.Now;
            vacancy.ClosingDate = DateTime.Now.AddDays(40);

            await _vacanciesRepository.CreateAsync(vacancy);
            await _vacanciesRepository.SaveAsync();


        }



        public async Task UpdateVacancy(int id, VacanciesPutDTO vacanciesPutDTO)
        {
            var company = await _repository.GetAll().FirstOrDefaultAsync(c => c.Id == id);
            if (company == null)
            {
                throw new NotFoundException("No company found");
            }

            var category = await _categoryRepository.GetByIdAsync(vacanciesPutDTO.CategoryId);
            var jobType = await _jobTypeRepository.GetByIdAsync(vacanciesPutDTO.JobTypeId);
            var jobSchedule = await _jobScheduleRepository.GetByIdAsync(vacanciesPutDTO.JobScheduleId);


            var vacancy = _mapper.Map<Vacancy>(vacanciesPutDTO);
            vacancy.Company = company;
            vacancy.JobSchedule = jobSchedule;
            vacancy.JobType = jobType;
            vacancy.Category = category;
            vacancy.PostedOn = DateTime.Now;
            vacancy.ClosingDate = DateTime.Now.AddDays(40);

            _vacanciesRepository.Update(vacancy);
            await _vacanciesRepository.SaveAsync();
        }


        public async Task DeleteVacancy(int id, int vacancyId)
        {


            var exists = _vacanciesRepository.GetAll().FirstOrDefault(v => v.CompanyId == id);
            if (exists is null)
            {
                throw new BadRequestException("Bad");
            }

            var company = _repository.GetAll().Include(c => c.Vacancies)
       .FirstOrDefault(c => c.Id == id);

            if (company == null)
            {
                throw new NotFoundException("Company not found");
            }

            var vacancy = _vacanciesRepository.GetAll().FirstOrDefault(v => v.Id == vacancyId);
            if (vacancy == null)
            {
                throw new NotFoundException("Vacancy not found");
            }

            company.Vacancies.Remove(vacancy);
            _context.SaveChanges();
        }

        public async Task InviteCandidateToInterview(int vacancyId, int candidateId, InvitationPostDTO invitation)
        {
            Vacancy vacancy = await _vacanciesRepository.GetByIdAsync(vacancyId);
            if (vacancy == null) throw new NotFoundException($"There is no vacancy with id: {vacancyId}");
            CandidateResume candidate = await _candidateResumeRepository.GetByIdAsync(candidateId);
            if (candidate == null) throw new NotFoundException($"There is no candidate with id: {candidateId}");

            Applications application = _applicationRepository.GetAll().SingleOrDefault(x => x.VacancyId == vacancyId);

            if (application == null) throw new NotFoundException("There is no application for this vacancy");
            if (application.IsAccepted == true) throw new RepeatedChoiceException("You already rejected this candidate");
            if (application.CandidateResumeId != candidateId) throw new NotFoundException("No application found from candidate");
            if (application.VacancyId != vacancyId) throw new NotFoundException($"No application for vacancy {vacancyId}");

            var result = _mapper.Map<Invitation>(invitation);
            result.InterviewDate = invitation.InterviewDate;
            result.InterviewLocation = invitation.InterviewLocation;
            result.Message = invitation.Message;

            var previousCv = application.CV;

            await _mailService.SendEmailAsync(new DTOs.Account.MailRequestDTO
            {
                ToEmail = candidate.Email,
                Subject = $"Interview invitation for {vacancy.Name}",
                //Body = $"Interview date:{result.InterviewDate}\n{result.Message}\nTime:{result.InterviewDate}\nLocation:{result.InterviewLocation}"
                Body = $"<html><body><h2>Interview invitation for {vacancy.Name}</h2><p><strong>Interview date:</strong> {result.InterviewDate}</p>" +
                $"<p><strong>Message:</strong> {result.Message}</p><p><strong>Time:</strong> {result.InterviewDate}</p><p><strong>Location:</strong> {result.InterviewLocation}</p></body></html>"

            });

            application.IsAccepted = true;
            application.CandidateResume = candidate;
            application.Vacancy = vacancy;
            application.CandidateResumeId = candidateId;
            application.VacancyId = vacancyId;
            application.CV = previousCv;

            _applicationRepository.Update(application);
            await _applicationRepository.SaveAsync();

        }

        public async Task RejectCandidate(int vacancyId, int candidateId)
        {
            Vacancy vacancy = await _vacanciesRepository.GetByIdAsync(vacancyId);
            CandidateResume candidate = await _candidateResumeRepository.GetByIdAsync(candidateId);
            Applications application = _applicationRepository.GetAll().SingleOrDefault(x => x.VacancyId == vacancyId);

            if (application == null) throw new NotFoundException("There is no application for this vacancy");
            if (application.IsAccepted == false) throw new RepeatedChoiceException("You already rejected this candidate");
            if (application.CandidateResumeId != candidateId) throw new NotFoundException("No application found from candidate");
            if (application.VacancyId != vacancyId) throw new NotFoundException($"No application for vacancy {vacancyId}");

            var previousCv = application.CV;

            await _mailService.SendEmailAsync(new DTOs.Account.MailRequestDTO
            {
                ToEmail = candidate.Email,
                Subject = $"Update on your {vacancy.Name} application ",
                Body = $"Dear {candidate.Fullname}.Thank you for your interest in the {vacancy.Name} position at" +
                $". After careful consideration, we have decided not to move forward with your application."

            });

            application.IsAccepted = false;
            application.CandidateResume = candidate;
            application.Vacancy = vacancy;
            application.CandidateResumeId = candidateId;
            application.VacancyId = vacancyId;
            application.CV = previousCv;

            _applicationRepository.Update(application);
            await _applicationRepository.SaveAsync();

        }

    }
}
