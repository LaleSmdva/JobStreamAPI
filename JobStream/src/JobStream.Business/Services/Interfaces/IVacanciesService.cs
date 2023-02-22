using JobStream.Business.DTOs.CompanyDTO;
using JobStream.Business.DTOs.VacanciesDTO;
using JobStream.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Services.Interfaces
{
	public interface IVacanciesService
	{
		//Task<string> ApplyVacancy(VacanciesDTO vacancyId, CandidateResume candidateResume);

		List<VacanciesDTO> GetAll();
		Task<VacanciesDTO> GetByIdAsync(int id);
		List<VacanciesDTO> GetByCondition(Expression<Func<Vacancy, bool>> expression);
		Task CreateAsync(VacanciesPostDTO entity);
		Task Update(int id, VacanciesPutDTO company);
		Task Delete(int id);
	}
}
