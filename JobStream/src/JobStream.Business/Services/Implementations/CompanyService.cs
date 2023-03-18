using AutoMapper;
using JobStream.Business.DTOs.CompanyDTO;
using JobStream.Business.DTOs.InvitationDTO;
using JobStream.Business.DTOs.VacanciesDTO;
using JobStream.Business.Exceptions;
using JobStream.Business.HelperServices.Interfaces;
using JobStream.Business.Services.Interfaces;
using JobStream.Business.Utilities;
using JobStream.Core.Entities;
using JobStream.Core.Entities.Identity;
using JobStream.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System.Text.Json.Serialization;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;
using JobStream.Business.DTOs.ApplicationsDTO;

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
        private readonly UserManager<AppUser> _userManager;
        private readonly IJobTypeRepository _jobTypeRepository;
        private readonly IJobScheduleRepository _jobScheduleRepository;
        private readonly ICandidateResumeRepository _candidateResumeRepository;
        private readonly IMailService _mailService;
        private readonly IApplicationRepository _applicationRepository;
        private readonly ICompanyRepository _companyRepository;


        public CompanyService(ICompanyRepository repository, IMapper mapper,
            IWebHostEnvironment environment, IFileService fileService, IVacanciesRepository vacanciesRepository,
            ICategoryRepository categoryRepository, ICompanyAndCategoryRepository companyAndCategoryRepository, UserManager<AppUser> userManager, IJobTypeRepository jobTypeRepository, IJobScheduleRepository jobScheduleRepository, ICandidateResumeRepository candidateResumeRepository, IMailService mailService, IApplicationRepository applicationRepository, ICompanyRepository companyRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _environment = environment;
            _fileService = fileService;
            _vacanciesRepository = vacanciesRepository;
            _categoryRepository = categoryRepository;
            _companyAndCategoryRepository = companyAndCategoryRepository;
            _userManager = userManager;
            _jobTypeRepository = jobTypeRepository;
            _jobScheduleRepository = jobScheduleRepository;
            _candidateResumeRepository = candidateResumeRepository;
            _mailService = mailService;
            _applicationRepository = applicationRepository;
            _companyRepository = companyRepository;
        }


        public async Task<List<CompanyDTO>> GetAllAsync()
        {
            var companies = await _repository.GetAll()
                //.Include(v => v.Vacancies)
                //.Include(x => x.CompanyAndCategories)
                .Where(x => x.IsDeleted != true)
                 .ToListAsync();
            var result = _mapper.Map<List<CompanyDTO>>(companies);
            return result;
        }


        public List<CompanyDTO> GetCompaniesByName(string companyName)
        {
            var company = _repository.GetAll().Where(n => n.Name.Contains(companyName))
                .Where(b => b.IsDeleted != true).ToList();
            if (company.Count() == 0) throw new NotFoundException("Not found");
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


        public async Task UpdateCompanyAccount(string id, List<int> deletedCategoryId, CompanyPutDTO companyPutDTO)
        {
            /// Updating Company

            Company company = await _repository.GetAll().FirstOrDefaultAsync(x => x.UserId == id);

            if (company is null || company.IsDeleted == true) throw new NotFoundException("Company not found");


            if (!companyPutDTO.Logo.CheckFileFormat("image/"))
            {
                throw new FileFormatException("Choose an image type");
            }

            if (company.UserId != id)
                throw new BadRequestException("Id's do not match");


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


            var appUser = await _userManager.FindByIdAsync(id);
            appUser.Companyname = companyPutDTO.Name;
            appUser.InfoCompany = companyPutDTO.AboutCompany;
            await _userManager.UpdateAsync(appUser);

            _repository.Update(company);
            await _repository.SaveAsync();

            // Add Category

            //List<CompanyAndCategory> companyAndCategoryList = new();
            //foreach (var catID in addedCategoryId)
            //{
            //    CompanyAndCategory companyAndCategory = new();
            //    if ((companyAndCategory.CategoryId == catID) && (companyAndCategory.CompanyId == company.Id))
            //    {
            //        throw new RepeatedChoiceException("You already added that category");
            //    }
            //    companyAndCategory.CategoryId = catID;
            //    companyAndCategory.CompanyId = company.Id;
            //    companyAndCategoryList.Add(companyAndCategory);



            //    foreach (var item in companyAndCategoryList)
            //    {
            //        await _companyAndCategoryRepository.CreateAsync(item);
            //    }
            //    await _companyAndCategoryRepository.SaveAsync();
            //}

            // Delete  from companyAndCategory if no vacancies exist with given categoryId in company
            foreach (var categoryId in deletedCategoryId)
            {
                //Throws exception if category contains vacancies
                //if (await _categoryRepository.GetByCondition(x => x.Id == categoryId && x.Id==).AllAsync(x => x.Vacancies.Count != 0))
                if (_vacanciesRepository.GetAll().Any(x => x.CategoryId == categoryId && x.CompanyId==company.Id))
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
            user.IsDeleted = true;

            var vacancies=_vacanciesRepository.GetAll().Where(v=>v.CompanyId==id).ToList();
            foreach (var vacancy in vacancies)
            {
                vacancy.isDeleted = true;
                _vacanciesRepository.Update(vacancy);
            }
            await _vacanciesRepository.SaveAsync();
            //_repository.Delete(result);
            await _repository.SaveAsync();
        }

        public async Task AddVacancyToCompany(int companyId, VacanciesPostDTO vacanciesPostDTO)
        {
            var company = await _repository.GetAll().FirstOrDefaultAsync(c => c.Id == companyId);
            if (company == null)
            {
                throw new NotFoundException("No company found");
            }
            var vacancy = _mapper.Map<Vacancy>(vacanciesPostDTO);
            if (vacancy == null)
            {
                throw new NotFoundException("No vacancy found");
            }
            var isExists = await _vacanciesRepository.GetAll().Where(x => x.Name == vacanciesPostDTO.Name && x.CompanyId == companyId).FirstOrDefaultAsync();
            if (isExists != null) throw new AlreadyExistsException("Vacancy with same name already exists");


            var category = await _categoryRepository.GetByIdAsync(vacanciesPostDTO.CategoryId);
            var jobType = await _jobTypeRepository.GetByIdAsync(vacanciesPostDTO.JobTypeId);
            var jobSchedule = await _jobScheduleRepository.GetByIdAsync(vacanciesPostDTO.JobScheduleId);
            if (jobType is null || jobSchedule is null || category is null)
            {
                throw new BadRequestException("Enter valid id");
            }

            vacancy.Company = company;
            vacancy.JobSchedule = jobSchedule;
            vacancy.JobType = jobType;
            vacancy.Category = category;
            vacancy.CompanyId = companyId;
            vacancy.JobScheduleId = jobSchedule.Id;
            vacancy.JobTypeId = jobType.Id;
            vacancy.PostedOn = DateTime.Now;
            //vacancy.ModifiedDate = DateTime.Now;
            vacancy.ClosingDate = DateTime.Now.AddDays(40);
            vacancy.CategoryId = category.Id;
            vacancy.isDeleted = false;



            await _vacanciesRepository.CreateAsync(vacancy);
            await _vacanciesRepository.SaveAsync();
        }


        public async Task UpdateVacancy(int companyId, int vacancyId, VacanciesPutDTO vacanciesPutDTO)
        {
            var vacancyExistsInCompany = _vacanciesRepository.GetAll().Where(x => x.CompanyId == companyId).Where(x => x.Id == vacancyId);
            if (vacancyExistsInCompany.Count() == 0)
            {
                throw new NotFoundException($"That vacancy id {vacancyId} doesn'exist in company with id:{companyId}");
            }
            var vacancy = await _vacanciesRepository.GetByIdAsync(vacancyId);
            var company = await _repository.GetByIdAsync(companyId);
            var category = await _categoryRepository.GetByIdAsync(vacanciesPutDTO.CategoryId);
            var jobSchedule = await _jobScheduleRepository.GetByIdAsync(vacanciesPutDTO.JobScheduleId);
            var jobType = await _jobTypeRepository.GetByIdAsync(vacanciesPutDTO.JobTypeId);
            //var test = _vacanciesRepository.GetAll().Where(x => x.CompanyId == companyId).Where(x => x.Id == vacancyId).ToList();

            if (company == null)
            {
                throw new NotFoundException($"Company with ID {companyId} not found.");
            }
            if (category == null)
            {
                throw new NotFoundException($"Category with ID {vacanciesPutDTO.CategoryId} not found.");
            }
            if (await _jobTypeRepository.GetAll().AllAsync(x => x.Id != vacanciesPutDTO.JobTypeId))
            {
                throw new NotFoundException("Job type with that id not found");
            }
            if (await _jobScheduleRepository.GetAll().AllAsync(x => x.Id != vacanciesPutDTO.JobScheduleId))
            {
                throw new NotFoundException("Job schedule with that id not found");
            }
            if (vacancyId != vacanciesPutDTO.Id)
            {
                throw new NotFoundException("Id's don't match");
            }

            //var vacancy = _mapper.Map<Vacancy>(vacanciesPutDTO);

            var creationDate = vacancy.PostedOn;
            var closingDate = vacancy.ClosingDate;

            vacancy.Name = vacanciesPutDTO.Name;
            vacancy.Location = vacanciesPutDTO.Location;
            vacancy.JobLocation = vacanciesPutDTO.JobLocation;
            vacancy.Salary = vacanciesPutDTO.Salary;
            vacancy.Requirements = vacanciesPutDTO.Requirements;
            vacancy.Description = vacanciesPutDTO.Description;
            vacancy.ExperienceLevel = vacanciesPutDTO.ExperienceLevel;
            vacancy.HREmail = vacanciesPutDTO.HREmail;
            vacancy.OfferedBenfits = vacanciesPutDTO.OfferedBenfits;


            vacancy.PostedOn = creationDate;
            vacancy.ClosingDate = closingDate;
            //vacancy.ModifiedDate = DateTime.Now;

            vacancy.Category = category;
            vacancy.Company = company;
            vacancy.JobSchedule = jobSchedule;
            vacancy.JobType = jobType;

            vacancy.isDeleted = false;


            _vacanciesRepository.Update(vacancy);
            await _vacanciesRepository.SaveAsync();
        }


        public async Task DeleteVacancy(int id, int vacancyId)
        {
            var vacancyExistsInCompany = _vacanciesRepository.GetAll().Where(x => x.CompanyId == id).Where(x => x.Id == vacancyId);
            if (vacancyExistsInCompany.Count() == 0)
            {
                throw new NotFoundException($"That vacancy id {vacancyId} doesn'exist in company with id:{id}");
            }

            var company = _repository.GetAll().Include(c => c.Vacancies).FirstOrDefault(c => c.Id == id);

            if (company == null)
            {
                throw new NotFoundException("Company not found");
            }

            var vacancy = _vacanciesRepository.GetAll().FirstOrDefault(v => v.Id == vacancyId);
            if (vacancy == null)
            {
                throw new NotFoundException("Vacancy not found");
            }

            _vacanciesRepository.Delete(vacancy);
            await _vacanciesRepository.SaveAsync();
        }
      
        public async Task InviteCandidateToInterview(int vacancyId, int candidateId, InvitationPostDTO invitation)
        {
            Vacancy vacancy = await _vacanciesRepository.GetByIdAsync(vacancyId);
            if (vacancy == null) throw new NotFoundException($"There is no vacancy with id: {vacancyId}");
            CandidateResume candidate = await _candidateResumeRepository.GetByIdAsync(candidateId);
            if (candidate == null) throw new NotFoundException($"There is no candidate with id: {candidateId}");

            Applications application = _applicationRepository.GetAll().SingleOrDefault(x => x.VacancyId == vacancyId);

            if (application == null) throw new NotFoundException("There is no application for this vacancy");
            if (application.IsAccepted == true) throw new RepeatedChoiceException("You already accepted this candidate");
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
                $"<p><strong>Message:</strong> {result.Message}</p><p><strong>Location:</strong> {result.InterviewLocation}</p></body></html>"

            });

            application.IsAccepted = true;
            application.CandidateResume = candidate;
            application.Vacancy = vacancy;
            application.CandidateResumeId = candidateId;
            application.VacancyId = vacancyId;
            application.CV = previousCv;

            var appDTO = _mapper.Map<ApplicationsDTO>(application);
            appDTO.IsAccepted= true;    

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

            var appDTO = _mapper.Map<ApplicationsDTO>(application);
            appDTO.IsAccepted = true;


            _applicationRepository.Update(application);
            await _applicationRepository.SaveAsync();

        }

       

        public async Task<List<ApplicationsDTO>> GetAllApplications(int companyId)
        {

            List<Vacancy> vacancy = _vacanciesRepository.GetByCondition(x => x.CompanyId == companyId).AsNoTracking()
                .ToList();
            List<Applications> list = new List<Applications>();
            foreach (var vac in vacancy)
            {
                var result = _applicationRepository.GetAll().FirstOrDefault(x => x.VacancyId == vac.Id);
                if (result != null)
                {
                    list.Add(result);
                }
            }
            if (list is null)
            {
                throw new NotFoundException("No applications found");
            }
            var listDTO=_mapper.Map<List<ApplicationsDTO>>(list);
            return listDTO;

        }

    }


}


