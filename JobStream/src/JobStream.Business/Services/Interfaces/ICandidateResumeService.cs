using JobStream.Business.DTOs.ApplicationsDTO;
using JobStream.Business.DTOs.ApplyVacancyDTO;
using JobStream.Business.DTOs.CandidateEducationDTO;
using JobStream.Business.DTOs.CandidateResumeDTO;
using JobStream.Business.DTOs.VacanciesDTO;
using JobStream.Core.Entities;
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
        Task<List<CandidateResumeDTO>> GetAllCandidatesResumesAsync();
        Task<CandidateResumeDTO> CandidateResumeDetails(int candidateId); 
        Task<CandidateResumeDTO> GetCandidateResumeByUserId(string userId);
        // User registeri
        //Task CreateCandidateResumeAsync(CandidateResumePostDTO entity);
        Task UpdateCandidateResumeAsync(int id, CandidateResumePutDTO candidateResume);
        Task DeleteCandidateResume(int id); //delete account
        Task ApplyVacancy(int candidateId, int companyId, int vacancyId, ApplyVacancyDTO applyVacancyDTO);
        Task<List<ApplicationsResponseDTO>> ViewStatusOfAppliedJobs(int candidateId);
        Task<List<ApplicationsResponseDTO>> GetAcceptedVacancies(int candidateId);
        Task<List<ApplicationsResponseDTO>> GetRejectedVacancies(int candidateId);


    }

}
