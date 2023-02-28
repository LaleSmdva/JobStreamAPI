using JobStream.Business.DTOs.ArticleDTO;
using JobStream.Business.DTOs.CompanyDTO;
using JobStream.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Services.Interfaces;

public interface IArticleService
{
	Task<List<ArticleDTO>> GetAll();

    Task<ArticleDTO> GetArticleByIdAsync(int id);
	List<ArticleDTO> GetArticlesByTitle(string title);
	Task<List<ArticleDTO>> GetArticlesByRubricId(int id);
	Task CreateArticleAsync(ArticlePostDTO entity);
	Task UpdateArticleAsync(int id, ArticlePutDTO article);
	Task DeleteArticleAsync(int id);
}
