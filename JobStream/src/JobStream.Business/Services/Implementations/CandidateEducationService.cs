using AutoMapper;
using JobStream.Business.DTOs.ArticleDTO;
using JobStream.Business.DTOs.CandidateEducationDTO;
using JobStream.Business.Exceptions;
using JobStream.Business.Services.Interfaces;
using JobStream.Core.Entities;
using JobStream.Core.Entities.Identity;
using JobStream.DataAccess.Repositories.Implementations;
using JobStream.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Services.Implementations
{
    public class CandidateEducationService : ICandidateEducationService
    {
        private readonly ICandidateEducationRepository _candidateEducationRepository;
        private readonly ICandidateResumeRepository _candidateResumeRepository;
        private readonly IMapper _mapper;

        public CandidateEducationService(IMapper mapper, ICandidateEducationRepository candidateEducationRepository, ICandidateResumeRepository candidateResumeRepository)
        {

            _mapper = mapper;
            _candidateEducationRepository = candidateEducationRepository;
            _candidateResumeRepository = candidateResumeRepository;
        }

        public async Task<List<CandidateEducationDTO>> GetAllCandidatesEducationAsync()
        {
            var candidateEds = await _candidateEducationRepository.GetAll().ToListAsync();
            var list = _mapper.Map<List<CandidateEducationDTO>>(candidateEds);
            return list;
        }

        public async Task<CandidateEducationDTO> GetCandidateEducationByResumeIdAsync(int id)
        {
            CandidateResume resume = await _candidateResumeRepository.GetByIdAsync(id);
            if (resume == null) throw new NotFoundException("No data found");
            CandidateEducation education = await _candidateEducationRepository.GetAll().FirstOrDefaultAsync(c => c.CandidateResumeId == id);
            var result = _mapper.Map<CandidateEducationDTO>(education);
            return result;
        }
        public async Task CreateCandidateEducationeAsync(CandidateEducationPostDTO entity)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateCandidateEducationAsync(int id, CandidateEducationPutDTO education)
        {
            throw new NotImplementedException();
        }
        public async Task DeleteJobTypeAsync(int id)
        {
            throw new NotImplementedException();
        }

   
    }
}
