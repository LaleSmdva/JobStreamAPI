using JobStream.Business.DTOs.CompanyDTO;
using JobStream.Business.DTOs.JobScheduleDTO;
using JobStream.Business.DTOs.VacanciesDTO;
using JobStream.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Services.Interfaces;

public interface IJobScheduleService
{
    Task<List<JobScheduleDTO>> GetAllJobSchedulesAsync();
    Task<List<JobScheduleDTO>> GetJobScheduleByIdAsync(int id);
    Task<List<VacanciesDTO>> GetAllVacanciesByJobScheduleIdAsync(int id);
    Task CreateJobScheduleAsync(JobSchedulePostDTO entity);
    Task UpdateJobScheduleAsync(int id, JobSchedulePutDTO jobSchedulePutDTO);
    Task DeleteJobScheduleAsync(int id);
}
