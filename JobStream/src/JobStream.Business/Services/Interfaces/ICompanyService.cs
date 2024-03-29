﻿using JobStream.Business.DTOs.ApplicationsDTO;
using JobStream.Business.DTOs.CompanyDTO;
using JobStream.Business.DTOs.InvitationDTO;
using JobStream.Business.DTOs.VacanciesDTO;
using JobStream.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<List<CompanyDTO>> GetAllAsync();
        Task<CompanyDTO> GetByIdAsync(int id);
        List<CompanyDTO> GetCompaniesByName(string companyName);

        //register da olur
        //Task CreateAsync(CompanyPostDTO entity);
        Task UpdateCompanyAccount(string id, List<int> addedCategoryId, CompanyPutDTO companyPutDTO);
        Task DeleteCompany(int id);
        Task AddVacancyToCompany(int companyId, VacanciesPostDTO vacanciesPostDTO);
        Task UpdateVacancy(int companyId, int vacancyId, VacanciesPutDTO vacanciesPutDTO);
        Task DeleteVacancy(int id, int vacancyId);
        //Update vacancy qaldi
        Task InviteCandidateToInterview(int vacancyId, int candidateId, InvitationPostDTO invitation);
        Task RejectCandidate(int vacancyId, int candidateId);
        Task<List<ApplicationsDTO>> GetAllApplications(int companyId);
    }
}
