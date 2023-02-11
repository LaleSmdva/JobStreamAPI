using AutoMapper;
using JobStream.Business.DTOs.CompanyDTO;
using JobStream.Business.DTOs.VacanciesDTO;
using JobStream.Business.Exceptions;
using JobStream.Business.Services.Interfaces;
using JobStream.Core.Entities;
using JobStream.DataAccess.Repositories.Implementations;
using JobStream.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
			var vacancies = _vacanciesRepository.GetAll().ToList();
			var result = _mapper.Map<List<VacanciesDTO>>(vacancies);
			return result;
		}

		public List<VacanciesDTO> GetByCondition(Expression<Func<Vacancy, bool>> expression)
		{
			var vacancy = _vacanciesRepository.GetByCondition(expression).ToList();
			var result = _mapper.Map<List<VacanciesDTO>>(vacancy);
			return result;
		}

		public async Task<VacanciesDTO> GetByIdAsync(int id)
		{
			var vacancy = await _vacanciesRepository.GetByIdAsync(id);
			var result = _mapper.Map<VacanciesDTO>(vacancy);
			return result;
		}
		public async Task CreateAsync(VacanciesPostDTO entity)
		{
			var vacancies = _mapper.Map<Vacancy>(entity);
			await _vacanciesRepository.CreateAsync(vacancies);
			await _vacanciesRepository.SaveAsync();
		}

		public async Task Delete(int id)
		{
			var vacancy = await _vacanciesRepository.GetByIdAsync(id);
			var result = _mapper.Map<Vacancy>(vacancy);
			_vacanciesRepository.Delete(result);
			await _vacanciesRepository.SaveAsync();
		}

	

		public async Task Update(int id, VacanciesPutDTO vacancy)
		{
			var vacancies = _vacanciesRepository.GetByCondition(c => c.Id == vacancy.Id, false);
			if (vacancies == null) throw new NotFoundException("Not Found");
			if (id != vacancy.Id) throw new BadRequestException("Id is not valid");
			var result = _mapper.Map<Vacancy>(vacancy);
			_vacanciesRepository.Update(result);
			await _vacanciesRepository.SaveAsync();
		}


		
	}
}
