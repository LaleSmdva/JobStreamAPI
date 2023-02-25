﻿using AutoMapper;
using JobStream.Business.DTOs.CandidateEducationDTO;
using JobStream.Business.DTOs.CandidateResumeDTO;
using JobStream.Business.Exceptions;
using JobStream.Business.HelperServices.Interfaces;
using JobStream.Business.Services.Interfaces;
using JobStream.Core.Entities;
using JobStream.Core.Entities.Identity;
using JobStream.DataAccess.Repositories.Implementations;
using JobStream.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
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

        public CandidateResumeService(ICandidateResumeRepository candidateResumeRepository, IMapper mapper, IAccountService accountService1, UserManager<AppUser> userManager, IFileService fileService, IWebHostEnvironment env)
        {
            _candidateResumeRepository = candidateResumeRepository;
            _mapper = mapper;
            _accountService1 = accountService1;
            _userManager = userManager;
            _fileService = fileService;
            _env = env;
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
            var candidateResumes = _candidateResumeRepository.GetAll()
                .Include(e=>e.CandidateEducation);
            if (await candidateResumes.AllAsync(x => x.CandidateEducation.Id == entity.CandidateEducationId))
            {
                throw new BadRequestException("You already created education for resume");
            }
            
            if (entity.CV != null)
            {
                var fileName = await _fileService.CopyFileAsync(entity.CV, _env.WebRootPath, "images", "Resumes");

                CandidateResume resume = _mapper.Map<CandidateResume>(entity);
                resume.CV = fileName;
                //article.CandidateEducation
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

    }
}


