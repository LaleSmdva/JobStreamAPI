using AutoMapper;
using JobStream.Business.DTOs.CandidateResumeDTO;
using JobStream.Business.DTOs.CategoryDTO;
using JobStream.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Mappers
{

    public class CandidateResumeMapper : Profile
    {
        public CandidateResumeMapper()
        {
            CreateMap<CandidateResume, CandidateResumeDTO>().ReverseMap();
            CreateMap<CandidateResume, CandidateResumePostDTO>().ReverseMap();
            CreateMap<CandidateResume, CandidateResumePutDTO>().ReverseMap();
        }
    }

}
