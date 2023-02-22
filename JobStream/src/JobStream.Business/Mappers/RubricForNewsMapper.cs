using AutoMapper;
using JobStream.Business.DTOs.RubricForArticlesDTO;
using JobStream.Business.DTOs.RubricForNewsDTO;
using JobStream.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Mappers
{

    public class RubricForNewsMapper : Profile
    {
        public RubricForNewsMapper()
        {
            CreateMap<RubricForNews, RubricForNewsDTO>().ReverseMap();
            CreateMap<RubricForNews, RubricForNewsPostDTO>().ReverseMap();
            CreateMap<RubricForNews, RubricForNewsPutDTO>().ReverseMap();
        }

    }
}
