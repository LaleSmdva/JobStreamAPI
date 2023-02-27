using JobStream.Business.DTOs.ArticleDTO;
using JobStream.Business.DTOs.RubricForArticlesDTO;
using JobStream.Business.DTOs.RubricForNewsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Services.Interfaces
{
    public interface IRubricForArticlesService
    {
        Task<List<RubricForArticlesDTO>> GetAllAsync();
        Task CreateRubricForArticlesAsync(RubricForArticlesPostDTO entity);
        Task UpdateRubricForArticlesAsync(int id, RubricForArticlesPutDTO news);
        Task DeleteRubricForArticlesAsync(int id);
    }
}
