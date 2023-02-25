using JobStream.Business.DTOs.CandidateEducationDTO;
using JobStream.Business.DTOs.CandidateResumeDTO;
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
        Task<List<CandidateResumeDTO>> GetAllCandidatesResumesAsync();
        Task<CandidateResumeDTO> CandidateResumeDetails(int candidateId); //done
        Task<CandidateResumeDTO> GetCandidateResumeByUserId(string userId);
        // User yarananda yaranmalidi
        Task CreateCandidateResumeAsync(CandidateResumePostDTO entity);
        Task UpdateCandidateResumeAsync(int id, CandidateResumePutDTO candidateResume);
        Task DeleteCandidateResume(int id);
    }

}
