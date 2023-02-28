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
using JobStream.Business.Mappers;

namespace JobStream.Business.Services.Implementations;

public class ArticleService : IArticleService
{
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _environment;
    private readonly IFileService _fileService;
    private readonly IRubricForArticlesRepository _rubricForArticlesRepository;

    public ArticleService(IArticleRepository articleRepository, IMapper mapper, IWebHostEnvironment environment, IFileService fileService, IRubricForArticlesRepository rubricForArticlesRepository)
    {
        _articleRepository = articleRepository;
        _mapper = mapper;
        _environment = environment;
        _fileService = fileService;
        _rubricForArticlesRepository = rubricForArticlesRepository;
    }
    public async Task<List<ArticleDTO>> GetAll()
    {
        var articles = await _articleRepository.GetAll().ToListAsync();
        var list = _mapper.Map<List<ArticleDTO>>(articles);
        return list;
    }

    public List<ArticleDTO> GetArticlesByTitle(string title)
    {
        var article = _articleRepository.GetAll().Where(a => a.Title.Contains(title)).ToList();
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


    public async Task<List<ArticleDTO>> GetArticlesByRubricId(int id)
    {
        var rubricForArticle = await _rubricForArticlesRepository.GetByIdAsync(id);
        if (rubricForArticle == null) throw new NotFoundException("Not found");
        var articles = await _articleRepository.GetAll()
            .Where(x => x.RubricForArticlesId == id).ToListAsync();
        if (articles == null || articles.Count() == 0) throw new NotFoundException("Article not found");
        var result = _mapper.Map<List<ArticleDTO>>(articles);
        return result;
    }

    public async Task CreateArticleAsync(ArticlePostDTO entity)
    {
        if (entity == null)
        {
            throw new NullReferenceException("Article can't be null");
        }
        if (await _rubricForArticlesRepository.GetAll().AllAsync(r => r.Id != entity.RubricForArticlesId)) throw new NotFoundException("There is no rubric with that id");
        if (await _articleRepository.GetAll().AllAsync(n => n.Title == entity.Title)) throw new AlreadyExistsException("Article with same title already exists");
        var fileName = await _fileService.CopyFileAsync(entity.Image, _environment.WebRootPath, "images", "Articles");

        if (entity == null) throw new NullReferenceException("Article can't ne null");
        var articles = _mapper.Map<Article>(entity);
        articles.PostedOn = DateTime.Now;
        articles.Image = fileName;

        RubricForArticles rubricForArticles = await _rubricForArticlesRepository.GetByIdAsync(entity.RubricForArticlesId);
        rubricForArticles.Id = entity.RubricForArticlesId;
        await _rubricForArticlesRepository.SaveAsync();

        await _articleRepository.CreateAsync(articles);
        await _articleRepository.SaveAsync();
    }


    public async Task UpdateArticleAsync(int id, ArticlePutDTO article)
    {
        var Article = await _articleRepository.GetByIdAsync(id);
        var articles = _articleRepository.GetByCondition(a => a.Id == article.Id, false);
        if (articles == null) throw new NotFoundException($"There is no article with id: {id}");
        if (id != article.Id) throw new BadRequestException("Id's do not match");
        if (await _rubricForArticlesRepository.GetAll().AllAsync(a => a.Id != article.RubricForArticlesId)) throw new NotFoundException("No rubric for article found");
        var fileName = await _fileService.CopyFileAsync(article.Image, _environment.WebRootPath, "images", "Articles");

        var prevPostedOn = Article.PostedOn;

        Article.Image = fileName;
        Article.PostedOn = prevPostedOn;
        Article.Title = article.Title;
        Article.RubricForArticlesId = article.RubricForArticlesId;

        _articleRepository.Update(Article);
        await _articleRepository.SaveAsync();
    }
    public async Task DeleteArticleAsync(int id)
    {
        var articles = _articleRepository.GetAll().ToList();

        if (articles.All(x => x.Id != id))
        {
            throw new NotFoundException("Not Found");
        }
        var article = await _articleRepository.GetByIdAsync(id);
        await _fileService.DeleteFileAsync(article.Image, _environment.WebRootPath, "images", "Articles");
        _articleRepository.Delete(article);
        await _articleRepository.SaveAsync();


    }

}
