using AutoMapper;
using JobStream.Business.DTOs.CategoryDTO;
using JobStream.Business.DTOs.CompanyDTO;
using JobStream.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Mappers
{
    public class CategoryMapper : Profile
    {
        public CategoryMapper()
        {
            CreateMap<Category, CategoriesDTO>().ReverseMap();
            CreateMap<Category, CategoriesPostDTO>().ReverseMap();
            CreateMap<Category, CategoriesPutDTO>().ReverseMap();
        }
    }
}
