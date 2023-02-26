using AutoMapper;
using JobStream.Business.DTOs.AboutUsDTO;
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

            CreateMap<Applications, ApplyVacancyDTO>().ReverseMap();

        }
    }
}
