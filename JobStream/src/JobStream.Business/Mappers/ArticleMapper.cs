using AutoMapper;
using JobStream.Business.DTOs.AboutUsDTO;
using JobStream.Business.DTOs.ArticleDTO;
using JobStream.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Mappers;

public class ArticleMapper:Profile
{
    public ArticleMapper()
    {
        CreateMap<Article, ArticleDTO>().ReverseMap();
        CreateMap<Article, ArticlePostDTO>().ReverseMap();
        CreateMap<Article, ArticlePutDTO>().ReverseMap();

    }
}
