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

        Task<CandidateResumeDTO> CandidateResumeDetails(int candidateId);
    }

}
