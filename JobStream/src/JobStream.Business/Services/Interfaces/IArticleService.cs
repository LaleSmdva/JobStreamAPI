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
	Task<List<ArticleDTO>> GetAllAsync();

    Task<ArticleDTO> GetArticleByIdAsync(int id);
	List<ArticleDTO> GetByArticleTitle(string title,Expression<Func<Article, bool>> expression);
	Task CreateArticleAsync(ArticlePostDTO entity);
	Task UpdateArticleAsync(int id, ArticlePutDTO company);
	Task DeleteArticleAsync(int id);
}
