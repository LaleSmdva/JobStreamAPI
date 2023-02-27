using JobStream.Business.DTOs.NewsDTO;
using JobStream.Business.DTOs.RubricForNewsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Services.Interfaces
{
    public interface IRubricForNewsService
    {
        Task<List<RubricForNewsDTO>> GetAllAsync();
        Task CreateRubricForNewsAsync(RubricForNewsPostDTO entity);
        Task UpdateRubricForNewsAsync(int id, RubricForNewsPutDTO news);
        Task DeleteRubricForNewsAsync(int id);
    }
}
