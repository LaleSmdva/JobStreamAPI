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
		List<CompanyDTO> GetAllAsync();
		Task<CompanyDTO> GetByIdAsync(int id);
		List<CompanyDTO> GetByCondition(Expression<Func<Company, bool>> expression);
		Task CreateAsync(CompanyPostDTO entity);
		Task Update(int id, CompanyPutDTO company);
		Task Delete(int id);
		//Task SaveAsync();
	}
}
