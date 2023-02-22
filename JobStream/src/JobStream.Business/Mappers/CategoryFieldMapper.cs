using AutoMapper;
using JobStream.Business.DTOs.ArticleDTO;
using JobStream.Business.DTOs.CategoryFieldDTO;
using JobStream.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Mappers
{

    public class CategoryFieldMapper : Profile
    {
        public CategoryFieldMapper()
        {
            CreateMap<CategoryField, CategoryFieldDTO>().ReverseMap();
            CreateMap<CategoryField, CategoryFieldPostDTO>().ReverseMap();
            CreateMap<CategoryField, CategoryFieldPutDTO>().ReverseMap();
        }

    }
}
