using AutoMapper;
using JobStream.Business.DTOs.AboutUsDTO;
using JobStream.Business.DTOs.ApplicationsDTO;
using JobStream.Business.DTOs.ApplyVacancyDTO;
using JobStream.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Mappers
{

    public class ApplicationsMapper : Profile
    {
        public ApplicationsMapper()
        {

            CreateMap<Applications, ApplicationsDTO>().ReverseMap();
            CreateMap<Applications, ApplicationsPostDTO>().ReverseMap();
            CreateMap<Applications, ApplicationsPutDTO>().ReverseMap();

        }
    }
}
