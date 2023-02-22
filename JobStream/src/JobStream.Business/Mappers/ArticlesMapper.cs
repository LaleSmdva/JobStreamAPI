using AutoMapper;
using JobStream.Business.DTOs.ArticleDTO;
using JobStream.Core.Entities;

namespace JobStream.Business.Mappers
{

    public class ArticlesMapper : Profile
    {
        public ArticlesMapper()
        {
            CreateMap<Article, ArticleDTO>().ReverseMap();
            CreateMap<Article, ArticlePostDTO>().ReverseMap();
            CreateMap<Article, ArticlePutDTO>().ReverseMap();
        }

    }
}
