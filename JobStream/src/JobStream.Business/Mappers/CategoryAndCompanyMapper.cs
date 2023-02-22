using AutoMapper;
using JobStream.Business.DTOs.CategoryFieldDTO;
using JobStream.Business.DTOs.CompanyAndCategoryDTO;
using JobStream.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Mappers
{

    public class CategoryAndCompanyMapper : Profile
    {
        public CategoryAndCompanyMapper()
        {
            CreateMap<CompanyAndCategory, CompanyAndCategoryDTO>().ReverseMap();
            CreateMap<CompanyAndCategory,CompanyAndCategoryPostDTO >().ReverseMap();
            CreateMap<CompanyAndCategory, CompanyAndCategoryPutDTO>().ReverseMap();
        }

    }

}
