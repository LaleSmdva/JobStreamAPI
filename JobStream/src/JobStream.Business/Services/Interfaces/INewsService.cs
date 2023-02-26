using JobStream.Business.DTOs.ArticleDTO;
using JobStream.Business.DTOs.NewsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Services.Interfaces
{
    public interface INewsService
    {
        Task<List<NewsDTO>> GetAllAsync();

        Task<NewsDTO> GetNewsByIdAsync(int id);
        List<NewsDTO> GetNewsByTitle(string title);
        Task<List<NewsDTO>> GetNewsByRubricId(int id);
        Task CreateNewsAsync(NewsPostDTO entity);
        Task UpdateNewsAsync(int id, NewsPutDTO news);
        Task DeleteNewsAsync(int id);
    }
}
