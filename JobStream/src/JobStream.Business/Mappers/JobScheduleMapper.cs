using AutoMapper;
using JobStream.Business.DTOs.CategoryDTO;
using JobStream.Business.DTOs.JobScheduleDTO;
using JobStream.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Mappers
{
    public class JobScheduleMapper : Profile
    {
        public JobScheduleMapper()
        {
            CreateMap<JobSchedule, JobScheduleDTO>().ReverseMap();
            CreateMap<JobSchedule, JobSchedulePostDTO>().ReverseMap();
            CreateMap<JobSchedule, JobSchedulePutDTO>().ReverseMap();
        }
    }
}
