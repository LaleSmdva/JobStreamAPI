using AutoMapper;
using JobStream.Business.DTOs.ApplyVacancyDTO;
using JobStream.Business.DTOs.CandidateEducationDTO;
using JobStream.Business.DTOs.CandidateResumeDTO;
using JobStream.Business.DTOs.VacanciesDTO;
using JobStream.Business.Exceptions;
using JobStream.Business.HelperServices.Interfaces;
using JobStream.Business.Services.Interfaces;
using JobStream.Business.Utilities;
using JobStream.Core.Entities;
using JobStream.Core.Entities.Identity;
using JobStream.Core.Interfaces;
using JobStream.DataAccess.Repositories.Implementations;
using JobStream.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Services.Implementations
{

    public class CandidateResumeService : ICandidateResumeService
    {
        private readonly ICandidateResumeRepository _candidateResumeRepository;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService1;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _env;
        private readonly ICandidateEducationRepository _candidateEducationRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IVacanciesRepository _vacanciesRepository;
        private readonly ICandidateResumeAndVacancyRepository _candidateResumeAndVacancy;


        public CandidateResumeService(ICandidateResumeRepository candidateResumeRepository, IMapper mapper, IAccountService accountService1, UserManager<AppUser> userManager, IFileService fileService, IWebHostEnvironment env, ICandidateEducationRepository candidateEducationRepository, ICompanyRepository companyRepository, IVacanciesRepository vacanciesRepository, ICandidateResumeAndVacancyRepository candidateResumeAndVacancy)
        {
            _candidateResumeRepository = candidateResumeRepository;
            _mapper = mapper;
            _accountService1 = accountService1;
            _userManager = userManager;
            _fileService = fileService;
            _env = env;
            _candidateEducationRepository = candidateEducationRepository;
            _companyRepository = companyRepository;
            _vacanciesRepository = vacanciesRepository;
            _candidateResumeAndVacancy = candidateResumeAndVacancy;
        }


        public async Task<List<CandidateResumeDTO>> GetAllCandidatesResumesAsync()
        {
            var candidateResumes = await _candidateResumeRepository.GetAll()
                .Include(e => e.CandidateEducation)
                .Include(u => u.AppUser).ToListAsync();
            var list = _mapper.Map<List<CandidateResumeDTO>>(candidateResumes);
            return list;
        }

        public async Task<CandidateResumeDTO> GetCandidateResumeByUserId(string userId)
        {
            //private readonly UserManager<AppUser> _userManager;
            //CandidateResume resume = await _candidateResumeRepository.GetByIdAsync(id);
            var resume = await _candidateResumeRepository.GetAll().FirstOrDefaultAsync(u => u.AppUserId == userId);
            if (resume == null) throw new NotFoundException("No data found");


            //CandidateEducation education = await _candidateEducationRepository.GetAll()
            //    .FirstOrDefaultAsync(c => c.CandidateResumeId == id);
            //if (education == null) throw new NotFoundException("Not found");
            var result = _mapper.Map<CandidateResumeDTO>(resume);
            return result;
        }
        /// <summary>
        /// //
        /// </summary>
        /// <param name="candidateId"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<CandidateResumeDTO> CandidateResumeDetails(int candidateId)
        {

            var resume = await _candidateResumeRepository.GetByIdAsync(candidateId);
            if (resume.IsDeleted == true)
                throw new NotFoundException("Resume not found");

            if (resume == null) throw new NotFoundException("Candidate not found");

            CandidateResume candidateResume = _candidateResumeRepository.GetAll()
                .Include(e => e.CandidateEducation)
                .Include(u => u.AppUser).FirstOrDefault(e => e.Id == candidateId);
            var result = _mapper.Map<CandidateResumeDTO>(resume);

            return result;

        }


        //var result = _mapper.Map<CandidateResumeDTO>(resume, opt =>
        //{
        //    if(resume.AppUser.Companyname==null)
        //    {
        //        opt.BeforeMap((src, dest) =>
        //        {
        //            //dest.AppUser.CompanyName = null;

        //        });
        //        opt.AfterMap((src, dest) =>
        //        {
        //            dest.AppUser.CompanyName = null;
        //        });
        //    }

        //});

        public async Task CreateCandidateResumeAsync(CandidateResumePostDTO entity)
        {
            if (entity == null) throw new NullReferenceException("Candidate resume can't ne null");
            var user = await _userManager.FindByEmailAsync(entity.Email);
            if (user is null) throw new NotFoundException("User not found");

            var candidateResumes = _candidateResumeRepository
                .GetAll()
                .Include(e => e.CandidateEducation);
            //if (entity.Email == user.Email)
            //{
            //    throw new BadRequestException("Resume is already created");
            //}

            if (entity.CV != null)
            {
                var fileName = await _fileService.CopyFileAsync(entity.CV, _env.WebRootPath, "images", "Resumes");

                CandidateResume resume = _mapper.Map<CandidateResume>(entity);
                resume.CV = fileName;
                resume.AppUserId = user.Id;

                //if (_candidateEducationRepository.GetAll().All(x => x.CandidateResumeId == resume.Id))
                //    throw new BadRequestException("Education is already created");

                CandidateEducation candidateEducation = new()
                {
                    Major = entity.CandidateEducation.Major,
                    Degree = entity.CandidateEducation.Degree,
                    Institution = entity.CandidateEducation.Institution,
                };

                await _candidateResumeRepository.CreateAsync(resume);
            }
            await _candidateResumeRepository.SaveAsync();
        }




        public async Task UpdateCandidateResumeAsync(int id, CandidateResumePutDTO candidateResume)
        {
            var resume = _candidateResumeRepository.GetAll().FirstOrDefault(a => a.Id == id);
            //AppUser user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            //var userResume = _candidateResumeRepository.GetByCondition(u => u.AppUserId == id, false);
            if (resume is null || resume.IsDeleted == true) throw new BadRequestException("No resume found with that id");

            //user.Id = userId;
            //if (resume == null) throw new NotFoundException($"There is no candidate resume with id: {id}");
            //if (userId != resume.Id) throw new BadRequestException($"{resume.Id} was not found");

            var result = _mapper.Map<CandidateResume>(candidateResume);

            _candidateResumeRepository.Update(result);
            //resume.AppUserId = user.AppUserId;

            //await _userManager.UpdateAsync(user);
            await _candidateResumeRepository.SaveAsync();

        }

        public async Task DeleteCandidateResume(int id)
        {
            var candidateEducations = _candidateResumeRepository.GetAll().ToList();

            if (candidateEducations.All(x => x.Id != id))
            {
                throw new NotFoundException("Not Found");
            }

            var education = await _candidateResumeRepository.GetByIdAsync(id);
            education.IsDeleted = true;
            //_candidateResumeRepository.Delete(education);
            await _candidateResumeRepository.SaveAsync();
        }

        public async Task ApplyVacancy(int candidateId, int companyId, int vacancyId, ApplyVacancyDTO applyVacancyDTO)
        {
            if ((!applyVacancyDTO.CV.CheckFileFormat("application/vnd.ms-word/")) && (!applyVacancyDTO.CV.CheckFileFormat("application/pdf"))
              && (!applyVacancyDTO.CV.CheckFileFormat("application/msword")) && (!applyVacancyDTO.CV.CheckFileFormat("text/plain")))
                throw new FileFormatException("Choose one of these file formats (doc, docx, pdf, text)");

            var fileName = await _fileService.CopyFileAsync(applyVacancyDTO.CV, _env.WebRootPath, "images", "Resumes", "CV");

            var candidate = await _candidateResumeRepository.GetByIdAsync(candidateId);
            if (candidate == null)
            {
                throw new NotFoundException("Candidate not found");
            }
            var company = await _companyRepository.GetByIdAsync(companyId);
            if (company == null)
            {
                throw new NotFoundException("Company not found");
            }

            Vacancy vacancy = await _vacanciesRepository.GetByIdAsync(vacancyId);

            if (vacancy == null)
            {
                throw new NotFoundException("Vacancy not found");
            }
            if (vacancy.Applications == null)
            {
                vacancy.Applications = new List<Applications>();
            }
            //var result = _mapper.Map<Applications>(applyVacancyDTO);
            var application = new Applications
            {
                CandidateResumeId = candidateId,
                VacancyId = vacancyId,
                CandidateResume = candidate,
                Vacancy = vacancy,
                CV = fileName,
            };

            vacancy.Applications.Add(application);
            vacancy.Company = company;

            await _vacanciesRepository.SaveAsync();

            CandidateResumeAndVacancy candidateResumeAndVacancy = new();
            candidateResumeAndVacancy.CandidateResumeId = application.Id;
            candidateResumeAndVacancy.VacancyId = vacancyId;

            await _candidateResumeAndVacancy.CreateAsync(candidateResumeAndVacancy);
            await _candidateResumeRepository.SaveAsync();

            vacancy.isApplied = true;
        }

        public async Task<List<VacanciesDTO>> ViewAppliedJobs()
        {
            List<Vacancy> vacancies=await _vacanciesRepository.GetAll().Where(v => v.isApplied == true).ToListAsync();
            var list=_mapper.Map<List<VacanciesDTO>>(vacancies);
            return list;
        }
    }
}



