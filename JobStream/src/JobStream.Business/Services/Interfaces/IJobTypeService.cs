using JobStream.Business.DTOs.ArticleDTO;
using JobStream.Business.DTOs.JobTypeDTO;
using JobStream.Business.DTOs.VacanciesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Services.Interfaces;

public interface IJobTypeService
{
    //AddVacancyToJobType
    Task<List<JobTypeDTO>> GetAllJobTypesAsync();

    Task<JobTypeDTO> GetJobTypeByIdAsync(int id);
    Task<List<VacanciesDTO>> GetVacanciesByJobTypeId(int id);
    Task CreateJobTypeAsync(JobTypePostDTO entity);
    Task UpdateJobTypeAsync(int id, JobTypePutDTO company);
    Task DeleteJobTypeAsync(int id);
}
