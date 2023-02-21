using AutoMapper;
using JobStream.Business.DTOs.JobScheduleDTO;
using JobStream.Business.DTOs.JobTypeDTO;
using JobStream.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Mappers
{
    public class JobTypeMapper : Profile
    {
        public JobTypeMapper()
        {
            CreateMap<JobType, JobTypeDTO>().ReverseMap();
            CreateMap<JobType, JobTypePostDTO>().ReverseMap();
            CreateMap<JobType, JobTypePutDTO>().ReverseMap();
        }
    }
}
