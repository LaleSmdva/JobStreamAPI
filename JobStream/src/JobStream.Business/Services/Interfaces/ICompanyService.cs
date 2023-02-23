using JobStream.Business.DTOs.CompanyDTO;
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
		List<CompanyDTO> GetByCondition(Expression<Func<Company, bool>> expression);
		Task CreateAsync(CompanyPostDTO entity);
		//Task Update(int id, CompanyPutDTO company);
		Task Update(int id, int? vacancyId, CompanyPutDTO companyPutDTO);

        Task Delete(int id);
        Task DeleteVacancy(int id,int vacancyId);
		//Task SaveAsync();
	}
}
