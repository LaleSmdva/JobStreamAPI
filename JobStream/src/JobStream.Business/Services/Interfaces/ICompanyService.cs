using JobStream.Business.DTOs.CompanyDTO;
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
		List<CompanyDTO> GetAll();
		Task<CompanyDTO> GetById(int id);
		IQueryable<CompanyDTO> GetByCondition(Expression<Func<CompanyDTO, bool>> expression);
		Task CreateAsync(CompanyDTO entity);
		Task Update(int id, CompanyUpdateDTO company);
		Task Delete(int id);
		//Task SaveAsync();
	}
}
