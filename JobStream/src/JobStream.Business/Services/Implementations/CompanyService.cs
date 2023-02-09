using AutoMapper;
using JobStream.Business.DTOs.CompanyDTO;
using JobStream.Business.Services.Interfaces;
using JobStream.Core.Entities;
using JobStream.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Services.Implementations
{
	public class CompanyService : ICompanyService
	{
		private readonly ICompanyRepository _repository;
		private readonly IMapper _mapper;

		public CompanyService(ICompanyRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public List<CompanyDTO> GetAll()
		{
			var companies = _repository.GetAll().ToList();
			var result = _mapper.Map<List<CompanyDTO>>(companies);
			return result;
		}

		public async Task CreateAsync(CompanyDTO entity)
		{
			var companies=_mapper.Map<Company>(entity);
			await _repository.CreateAsync(companies);
			await _repository.SaveAsync();
		}

		public async Task Delete(int id)
		{
			var company=await _repository.GetByIdAsync(id);
			var result=_mapper.Map<Company>(company);
			_repository.Delete(result);
			await _repository.SaveAsync();
		}

	

		public IQueryable<CompanyDTO> GetByCondition(Expression<Func<CompanyDTO, bool>> expression)
		{
			throw new NotImplementedException();
		}

		public Task<CompanyDTO> GetById(int id)
		{
			throw new NotImplementedException();
		}


		public async Task Update(int id,CompanyUpdateDTO company)
		{
			var Company=await _repository.GetByIdAsync(id);
			var result=_mapper.Map<Company>(company);
			_repository.Update(result);
			await _repository.SaveAsync();
		}
	}
}
