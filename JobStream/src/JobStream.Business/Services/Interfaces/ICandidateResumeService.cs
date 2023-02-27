using JobStream.Business.DTOs.ApplicationsDTO;
using JobStream.Business.DTOs.ApplyVacancyDTO;
using JobStream.Business.DTOs.CandidateEducationDTO;
using JobStream.Business.DTOs.CandidateResumeDTO;
using JobStream.Business.DTOs.VacanciesDTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Services.Interfaces
{
    public interface ICandidateResumeService
    {

        //CandidateResumeDetails
        //TrackApplicationStatus
        //contactApplicant --company profile

        //SavedVacancies
        //ViewAppliedJobs
        //Viewvacancies
        //SearchForJob Filter-JobTitle,Location,Category
        Task<List<CandidateResumeDTO>> GetAllCandidatesResumesAsync();
        Task<CandidateResumeDTO> CandidateResumeDetails(int candidateId); //done
        Task<CandidateResumeDTO> GetCandidateResumeByUserId(string userId);
        // User yarananda yaranmalidi
        //Task CreateCandidateResumeAsync(CandidateResumePostDTO entity);
        Task UpdateCandidateResumeAsync(int id, CandidateResumePutDTO candidateResume);
        Task DeleteCandidateResume(int id); //delete account from website
        Task ApplyVacancy(int candidateId, int companyId, int vacancyId, ApplyVacancyDTO applyVacancyDTO);
        Task<List<ApplicationsDTO>> ViewAppliedJobs(int candidateId);
        //new 26
        //subscribe To company
        //Task Subscribe(int companyId);

    }

}
