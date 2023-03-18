using AutoMapper;
using JobStream.Business.DTOs.CandidateEducationDTO;
using JobStream.Business.Exceptions;
using JobStream.Business.Services.Interfaces;
using JobStream.Core.Entities;
using JobStream.DataAccess.Repositories;
using JobStream.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JobStream.Business.Services.Implementations
{
    public class CandidateEducationService :ICandidateEducationService
    {
       
        private readonly ICandidateEducationRepository _candidateEducationRepository;
        private readonly IMapper _mapper;


        public CandidateEducationService(IMapper mapper, ICandidateEducationRepository candidateEducationRepository)
        {
            _mapper = mapper;
            _candidateEducationRepository = candidateEducationRepository;
        }

        public async Task<List<CandidateEducationDTO>> GetAllCandidatesEducationAsync()
        {
            var candidateEds = await _candidateEducationRepository.GetAll().ToListAsync();
            var list = _mapper.Map<List<CandidateEducationDTO>>(candidateEds);
          
            return list;
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
   

        public async Task DeleteCandidateEducationInfoAsync(int candidateId, List<int> educationIds)
        {
            
            var candidate = await _candidateEducationRepository.GetAll().FirstOrDefaultAsync(c => c.Id == candidateId);

            if (candidate == null)
            {
                throw new ArgumentException("Candidate not found", nameof(candidateId));
            }
            List<CandidateEducation> deletedIds = new List<CandidateEducation>();
            foreach (var educationId in educationIds)
            {
                var educationInfo = await _candidateEducationRepository.GetByIdAsync(educationId);
                if (educationInfo == null)
                {
                    throw new NotFoundException("Education info not found");
                }

                List<CandidateEducation> deletedInfos = await _candidateEducationRepository.GetAll().Where(x => x.CandidateResumeId == candidateId).Where(x => x.Id == educationId).ToListAsync();
                if (deletedInfos.Count() != 0)
                {
                    foreach (var deletedInfo in deletedInfos)
                    {

                        deletedIds.Add(deletedInfo);
                    }
                }
            }

            foreach (var deletedId in deletedIds)
            {
                _candidateEducationRepository.Delete(deletedId);
            }
            await _candidateEducationRepository.SaveAsync();


        }


    }
}
