using JobStream.Business.DTOs.AboutUsDTO;
using JobStream.Business.DTOs.ArticleDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Services.Interfaces
{
    public interface IAboutUsService
    {
        Task<List<AboutUsDTO>> GetAboutUsAsync();
        Task CreateAboutUsAsync(AboutUsPostDTO entity);
        Task UpdateAboutUsAsync(int id, AboutUsPutDTO aboutUs);
        Task DeleteAboutUsAsync(int id);
    }
}
