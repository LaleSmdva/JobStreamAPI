﻿using AutoMapper;
using JobStream.Business.DTOs.CompanyDTO;
using JobStream.Business.Exceptions;
using JobStream.Business.Services.Interfaces;
using JobStream.Core.Entities;
using JobStream.DataAccess.Contexts;
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
		private readonly AppDbContext _context;

		public CompanyService(ICompanyRepository repository, IMapper mapper, AppDbContext context)
		{
			_repository = repository;
			_mapper = mapper;
			_context = context;
		}

		public List<CompanyDTO> GetAll()
		{
			var companies = _repository.GetAll().ToList();
			var result = _mapper.Map<List<CompanyDTO>>(companies);
			return result;
		}

		public async Task CreateAsync(CompanyPostDTO entity)
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

	

		public  List<CompanyDTO> GetByCondition(Expression<Func<Company, bool>> expression)
		{
			
			var company=_repository.GetByCondition(expression).ToList();
			var result=_mapper.Map<List<CompanyDTO>>(company);
			return result;
		}

		public async Task<CompanyDTO> GetById(int id)
		{
			var company=await _repository.GetByIdAsync(id);
			var result = _mapper.Map<CompanyDTO>(company);
			return result;
		}


		public async Task Update(int id,CompanyPutDTO company)
		{
			var Company =  _repository.GetByCondition(c => c.Id==company.Id, false);
			if (Company == null) throw new NotFoundException("Not Found");
			if (id != company.Id) throw new BadRequestException("Id is not valid");
			var result=_mapper.Map<Company>(company);
			_repository.Update(result);
			await _repository.SaveAsync();
		}
	}
}
