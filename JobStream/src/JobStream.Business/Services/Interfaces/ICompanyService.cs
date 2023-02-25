using JobStream.Business.DTOs.CompanyDTO;
using JobStream.Business.DTOs.VacanciesDTO;
using JobStream.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Services.Interfaces
{
	public interface ICompanyService
	{
		Task<List<CompanyDTO>> GetAllAsync();
		Task<CompanyDTO> GetByIdAsync(int id);
		List<CompanyDTO> GetCompaniesByName(string companyName);
		Task CreateAsync(CompanyPostDTO entity);
		Task Update(int id, List<int> addedCategoryId, List<int> deletedCategoryId, CompanyPutDTO companyPutDTO);
        Task Delete(int id);
		Task AddVacancy(int id, VacanciesPostDTO vacanciesPostDTO);
        Task DeleteVacancy(int id,int vacancyId);
	}
}
