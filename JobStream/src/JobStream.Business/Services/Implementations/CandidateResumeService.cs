using AutoMapper;
using JobStream.Business.DTOs.CandidateResumeDTO;
using JobStream.Business.Exceptions;
using JobStream.Business.Services.Interfaces;
using JobStream.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Services.Implementations
{
    public class CandidateResumeService:ICandidateResumeService
    {
        private readonly ICandidateResumeRepository _candidateResumeRepository;
        private readonly IMapper _mapper;

        public CandidateResumeService(ICandidateResumeRepository candidateResumeRepository, IMapper mapper)
        {
            _candidateResumeRepository = candidateResumeRepository;
            _mapper = mapper;
        }

        public async Task<CandidateResumeDTO> CandidateResumeDetails(int candidateId)
        {
           var candidateResume=await _candidateResumeRepository.GetByIdAsync(candidateId);
            if (candidateResume == null) throw new NotFoundException("Candidate not found");

            var result=_mapper.Map<CandidateResumeDTO>(candidateResume);
            return result;
        }
    }
}
