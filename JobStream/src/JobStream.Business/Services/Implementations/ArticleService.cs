using AutoMapper;
using JobStream.Business.DTOs.CompanyDTO;
using JobStream.Business.HelperServices.Interfaces;
using JobStream.Business.Services.Interfaces;
using JobStream.Core.Entities;
using JobStream.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Services.Implementations;

public class ArticleService : IArticleService
{
	private readonly IArticleRepository _articleRepository;
	private readonly IMapper _mapper;
	private readonly IWebHostEnvironment _environment;
	private readonly IFileService _fileService;

	public ArticleService(IArticleRepository articleRepository, IMapper mapper, IWebHostEnvironment environment, IFileService fileService)
	{
		_articleRepository = articleRepository;
		_mapper = mapper;
		_environment = environment;
		_fileService = fileService;
	}

	public Task CreateAsync(CompanyPostDTO entity)
	{
		throw new NotImplementedException();
	}

	public Task Delete(int id)
	{
		throw new NotImplementedException();
	}

	public List<CompanyDTO> GetAll()
	{
		throw new NotImplementedException();
	}

	public List<CompanyDTO> GetByCondition(Expression<Func<Company, bool>> expression)
	{
		throw new NotImplementedException();
	}

	public Task<CompanyDTO> GetByIdAsync(int id)
	{
		throw new NotImplementedException();
	}

	public Task Update(int id, CompanyPutDTO company)
	{
		throw new NotImplementedException();
	}
}
