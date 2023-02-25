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

        public CandidateEducationService(IMapper mapper,
            ICandidateEducationRepository candidateEducationRepository,
            ICandidateResumeRepository candidateResumeRepository)
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
          
            CandidateEducation education = await _candidateEducationRepository.GetAll()
                .FirstOrDefaultAsync(c => c.CandidateResumeId == id);
            if (education == null) throw new NotFoundException("Not found");
            var result = _mapper.Map<CandidateEducationDTO>(education);
            return result;
        }
        public async Task CreateCandidateEducationeAsync(CandidateEducationPostDTO entity)
        {
            //if (entity == null) throw new NullReferenceException("Candidate education can't ne null");
            //var candEds = _candidateEducationRepository.GetAll();
            //if (await candEds.AllAsync(x => x.CandidateResumeId == entity.CandidateResumeId))
            //{
            //    throw new BadRequestException("You already created education for resume");
            //}
            //var article = _mapper.Map<CandidateEducation>(entity);
            //await _candidateEducationRepository.CreateAsync(article);
            //await _candidateEducationRepository.SaveAsync();
        }

        public async Task UpdateCandidateEducationAsync(int id, CandidateEducationPutDTO education)
        {
            var candidateEducation = _candidateEducationRepository.GetByCondition(a => a.Id == education.Id, false);
            if (candidateEducation== null) throw new NotFoundException($"There is no candidate education with id: {id}");
            if (id != education.Id) throw new BadRequestException($"{education.Id} was not found");

            var result = _mapper.Map<CandidateEducation>(education);
            _candidateEducationRepository.Update(result);
            await _candidateEducationRepository.SaveAsync();
        }
        public async Task DeleteCandidateEducation(int id)
        {
            var candidateEducations = _candidateEducationRepository.GetAll().ToList();

            if (candidateEducations.All(x => x.Id != id))
            {
                throw new NotFoundException("Not Found");
            }
            var education = await _candidateEducationRepository.GetByIdAsync(id);
            _candidateEducationRepository.Delete(education);
            await _candidateEducationRepository.SaveAsync();
        }

   
    }
}
