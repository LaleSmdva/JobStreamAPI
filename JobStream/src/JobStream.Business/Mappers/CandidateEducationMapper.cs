using AutoMapper;
using JobStream.Business.DTOs.ArticleDTO;
using JobStream.Business.DTOs.CandidateEducationDTO;
using JobStream.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Mappers
{
    public class CandidateEducationMapper : Profile
    {
        public CandidateEducationMapper()
        {
            CreateMap<CandidateEducation, CandidateEducationDTO>().ReverseMap();
            CreateMap<CandidateEducation, CandidateEducationPostDTO>().ReverseMap();
            CreateMap<CandidateEducation, CandidateEducationPutDTO>().ReverseMap();
        }
    }
}
