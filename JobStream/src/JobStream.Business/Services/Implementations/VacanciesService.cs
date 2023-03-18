using AutoMapper;
using JobStream.Business.DTOs.VacanciesDTO;
using JobStream.Business.Exceptions;
using JobStream.Business.Services.Interfaces;
using JobStream.Core.Entities;
using JobStream.DataAccess.Repositories.Interfaces;

namespace JobStream.Business.Services.Implementations
{
    public class VacanciesService : IVacanciesService
    {
        private readonly IVacanciesRepository _vacanciesRepository;
        private readonly IMapper _mapper;

        public VacanciesService(IMapper mapper, IVacanciesRepository vacanciesRepository)
        {
            _mapper = mapper;
            _vacanciesRepository = vacanciesRepository;
        }

        public List<VacanciesDTO> GetAll()
        {
            var vacancies = _vacanciesRepository.GetAll()
                .Where(v => v.isDeleted == false).ToList();
            var result = _mapper.Map<List<VacanciesDTO>>(vacancies);
            return result;
        }

        public async Task<List<VacanciesDTO>> GetVacanciesByCategoryAsync(List<int> categoryIds)
        {
            List<Vacancy> vacanciesDTOs = new List<Vacancy>();
            foreach (var categoryId in categoryIds)
            {
                var vacancies = _vacanciesRepository.GetByCondition(x => x.CategoryId == categoryId);
                foreach (var vacancy in vacancies)
                {
                    if (vacancy != null)
                    {
                        vacanciesDTOs.Add(vacancy);
                    }
                }
            }
            var list = _mapper.Map<List<VacanciesDTO>>(vacanciesDTOs);

            return list;

        }

        public async Task<VacanciesDTO> GetVacancyByIdAsync(int id)
        {
            var vacancy = await _vacanciesRepository.GetByIdAsync(id);
            var result = _mapper.Map<VacanciesDTO>(vacancy);
            return result;
        }

        public IEnumerable<Vacancy> GetExpiredVacancies()
        {
            var currentDateUtc = DateTime.Now;
            return _vacanciesRepository.GetAll().Where(v => v.ClosingDate <= currentDateUtc);
        }


        ////////////  Hangfire  //////////// 

        //[AutomaticRetry(Attempts = 0)]
        public async Task VacancyCleanUp()
        {
            var expiredVacancies = GetExpiredVacancies();
            foreach (var vacancy in expiredVacancies)
            {
                //_vacanciesRepository.Delete(vacancy);
                vacancy.isDeleted = true;
                _vacanciesRepository.Update(vacancy);

            }
            await _vacanciesRepository.SaveAsync();
        }


        public async Task<List<VacanciesDTO>> SearchVacancies(string? keyword, string? location, List<int>? categoryId, string? companyName)
        {
            var vacancies = _vacanciesRepository.GetAll();

            if (!string.IsNullOrEmpty(keyword))
            {
                vacancies = vacancies.Where(x => x.Name.Contains(keyword));
            }

            if (!string.IsNullOrEmpty(location))
            {
                vacancies = vacancies.Where(x => x.Location.Contains(location));
            }

            if (categoryId != null && categoryId.Count > 0)
            {
                vacancies = vacancies.Where(x => categoryId.Contains(x.CategoryId));
            }

            if (!string.IsNullOrEmpty(companyName))
            {
                vacancies = vacancies.Where(x => x.Company.Name.Contains(companyName));
            }
            if (vacancies.Count() == 0)
            {
                throw new NotFoundException("Not found");
            }
            var result = _mapper.Map<List<VacanciesDTO>>(vacancies);
            return result;
        }
    }
}
