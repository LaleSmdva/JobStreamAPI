using JobStream.Business.DTOs.CandidateEducationDTO;
using JobStream.Business.DTOs.JobTypeDTO;
using JobStream.Business.DTOs.VacanciesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Services.Interfaces
{
    public interface ICandidateEducationService
    {
        Task<List<CandidateEducationDTO>> GetAllCandidatesEducationAsync();

        Task<CandidateEducationDTO> GetCandidateEducationByResumeIdAsync(int id);
        Task CreateCandidateEducationeAsync(CandidateEducationPostDTO entity);
        Task UpdateCandidateEducationAsync(int id, CandidateEducationPutDTO education);
        Task DeleteJobTypeAsync(int id);
    }
}
