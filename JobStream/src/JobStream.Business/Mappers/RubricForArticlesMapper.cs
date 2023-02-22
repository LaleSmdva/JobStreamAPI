using AutoMapper;
using JobStream.Business.DTOs.JobTypeDTO;
using JobStream.Business.DTOs.RubricForArticlesDTO;
using JobStream.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Mappers
{

    public class RubricForArticlesMapper : Profile
    {
        public RubricForArticlesMapper()
        {
            CreateMap<RubricForArticles, RubricForArticlesDTO>().ReverseMap();
            CreateMap<RubricForArticles, RubricForArticlesPostDTO>().ReverseMap();
            CreateMap<RubricForArticles, RubricForArticlesPutDTO>().ReverseMap();
        }
    }

}
