using AutoMapper;
using JobStream.Business.DTOs.ArticleDTO;
using JobStream.Business.DTOs.CompanyDTO;
using JobStream.Business.Exceptions;
using JobStream.Business.HelperServices.Interfaces;
using JobStream.Business.Services.Interfaces;
using JobStream.Core.Entities;
using JobStream.DataAccess.Repositories.Implementations;
using JobStream.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
    public async Task<List<ArticleDTO>> GetAllAsync()
    {
        var articles = await _articleRepository.GetAll().ToListAsync();
        var list = _mapper.Map<List<ArticleDTO>>(articles);
        return list;
    }

    public List<ArticleDTO> GetByArticleTitle(string title/*Expression<Func<Article, bool>> expression*/)
    {
        var article = _articleRepository.GetAll().Where(a=>a.Title.Contains(title)).ToList();
        if (article is null) throw new NotFoundException($"No title with name {title} found");
        var list = _mapper.Map<List<ArticleDTO>>(article);
        return list;
    }

    public async Task<ArticleDTO> GetArticleByIdAsync(int id)
    {
        var article = await _articleRepository.GetByIdAsync(id);
        if (article == null) throw new NotFoundException("No article found");
        var result = _mapper.Map<ArticleDTO>(article);
        return result;
    }

    public async Task CreateArticleAsync(ArticlePostDTO entity)
    {
        if (entity == null) throw new NullReferenceException("Article can't ne null");
        var article = _mapper.Map<Article>(entity);
        article.PostedOn= DateTime.Now; 
        await _articleRepository.CreateAsync(article);
        await _articleRepository.SaveAsync();
    }


    public async Task UpdateArticleAsync(int id, ArticlePutDTO article)
    {
        var articles=_articleRepository.GetByCondition(a => a.Id == article.Id,false);
        if (articles == null) throw new NotFoundException($"There is no article with id: {id}");
        if(id!=article.Id) throw new BadRequestException($"{article.Id} was not found");  

        var result = _mapper.Map<Article>(article);
        _articleRepository.Update(result);
        await _articleRepository.SaveAsync();
    }
    public async Task DeleteArticleAsync(int id)
    {
        var articles=_articleRepository.GetAll().ToList();
      
        if (articles.All(x => x.Id != id))
        {
            throw new NotFoundException("Not Found");
        }
        var article=await _articleRepository.GetByIdAsync(id);
        _articleRepository.Delete(article);
        await _articleRepository.SaveAsync();

      
    }
}
