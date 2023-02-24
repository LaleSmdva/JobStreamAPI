using AutoMapper;
using JobStream.Business.DTOs.ArticleDTO;
using JobStream.Business.DTOs.JobTypeDTO;
using JobStream.Business.DTOs.VacanciesDTO;
using JobStream.Business.Exceptions;
using JobStream.Business.Services.Interfaces;
using JobStream.Core.Entities;
using JobStream.DataAccess.Repositories.Implementations;
using JobStream.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Services.Implementations;

public class JobTypeService : IJobTypeService
{
    private readonly IJobTypeRepository _jobTypeRepository;
    private readonly IVacanciesRepository _vacanciesRepository;
    private readonly IMapper _mapper;

    public JobTypeService(IJobTypeRepository jobTypeRepository, IMapper mapper, IVacanciesRepository vacanciesRepository)
    {
        _jobTypeRepository = jobTypeRepository;
        _mapper = mapper;
        _vacanciesRepository = vacanciesRepository;
    }

    public async Task<List<JobTypeDTO>> GetAllJobTypesAsync()
    {
        var jobTypes = await _jobTypeRepository.GetAll()
            .Include(v=>v.Vacancies)
            .ToListAsync();
        var list = _mapper.Map<List<JobTypeDTO>>(jobTypes);
        return list;
    }

    public async Task<JobTypeDTO> GetJobTypeByIdAsync(int id)
    {
        //same as GetByid
        var jobTypes = await _jobTypeRepository.GetAll()
               .Include(v => v.Vacancies).ToListAsync();
        if (jobTypes.All(x => x.Id != id))
        {
            throw new NotFoundException("Not Found");
        }

        var jobType = await _jobTypeRepository.GetByIdAsync(id);
        if (jobType == null) throw new NotFoundException("No job type found");
        var result = _mapper.Map<JobTypeDTO>(jobType);
        return result;
    }

    public async Task<List<VacanciesDTO>> GetVacanciesByJobTypeId(int id)
    {
        var jobType=await _jobTypeRepository.GetByIdAsync(id);
        if (jobType == null) throw new NotFoundException("Not found");
        var vacancies =await  _vacanciesRepository.GetAll().Where(v => v.JobTypeId == id).ToListAsync();
        if (vacancies == null || vacancies.Count() == 0) throw new NotFoundException("Vacancy not found");
        var result = _mapper.Map<List<VacanciesDTO>>(vacancies);
        return result;
    }

    public async Task CreateJobTypeAsync(JobTypePostDTO entity)
    {
        if (entity == null) throw new NullReferenceException("Job type can't ne null");
        var jobType = _mapper.Map<JobType>(entity);
  
        await _jobTypeRepository.CreateAsync(jobType);
        await _jobTypeRepository.SaveAsync();
    }

    public async Task UpdateJobTypeAsync(int id, JobTypePutDTO jobType)
    {
        var jobTypes = _jobTypeRepository.GetByCondition(a => a.Id == jobType.Id, false);
        if (jobTypes == null) throw new NotFoundException($"There is no job type with id: {id}");
        if (id != jobType.Id) throw new BadRequestException($"{jobType.Id} was not found");

        var result = _mapper.Map<JobType>(jobType);
        _jobTypeRepository.Update(result);
        await _jobTypeRepository.SaveAsync();
    }

    public async Task DeleteJobTypeAsync(int id)
    {
        var jobTypes = _jobTypeRepository.GetAll().ToList();

        if (jobTypes.All(x => x.Id != id))
        {
            throw new NotFoundException("Not Found");
        }
        var jobType = await _jobTypeRepository.GetByIdAsync(id);
        _jobTypeRepository.Delete(jobType);
        await _jobTypeRepository.SaveAsync();
    }

 

  
}
