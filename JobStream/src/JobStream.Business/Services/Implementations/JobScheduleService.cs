using AutoMapper;
using JobStream.Business.DTOs.JobScheduleDTO;
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

public class JobScheduleService : IJobScheduleService
{
    private readonly IJobScheduleRepository _repository;
    private readonly IVacanciesRepository _vacanciesRepository;
    private readonly IMapper _mapper;

    public JobScheduleService(IJobScheduleRepository repository, IMapper mapper, IVacanciesRepository vacanciesRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _vacanciesRepository = vacanciesRepository;
    }

    public async Task<List<JobScheduleDTO>> GetAllJobSchedulesAsync()
    {
        var jobSchedules = await _repository.GetAll()
            .Include(v => v.Vacancies).ToListAsync();
        var result = _mapper.Map<List<JobScheduleDTO>>(jobSchedules);
        return result;
    }
    public async Task<List<JobScheduleDTO>> GetJobScheduleByIdAsync(int id)
    {
        var jobSchedule = await _repository.GetAll().Where(x => x.Id == id)
          .Include(v => v.Vacancies).ToListAsync();

        if (jobSchedule == null) throw new NotFoundException("Not found");
        var result = _mapper.Map<List<JobScheduleDTO>>(jobSchedule);
        return result;
    }

    public async Task<List<VacanciesDTO>> GetAllVacanciesByJobScheduleIdAsync(int id)
    {
        var jobSchedules = _repository.GetAll();
        var jobSchedule = await _repository.GetByIdAsync(id); //vacancies var(id)
        if (jobSchedule == null) throw new NotFoundException("Not found");

        var vacancies = _vacanciesRepository.GetAll().Where(x => x.JobScheduleId == id);
        if (vacancies == null || vacancies.Count() == 0) throw new NotFoundException("Vacancy not found");
        var result = _mapper.Map<List<VacanciesDTO>>(vacancies);
        return result;
    }


    public async Task CreateJobScheduleAsync(JobSchedulePostDTO entity)
    {
        if (entity == null) throw new NullReferenceException("Schedule name can't ne null");
        var jobSchedule = _mapper.Map<JobSchedule>(entity);
        await _repository.CreateAsync(jobSchedule);
        await _repository.SaveAsync();
    }

    public async Task UpdateJobScheduleAsync(int id, JobSchedulePutDTO jobSchedulePutDTO)
    {
        var schedules = _repository.GetByCondition(a => a.Id == jobSchedulePutDTO.Id, false);
        if (schedules == null) throw new NotFoundException("There is no article with id: ");
        if (id != jobSchedulePutDTO.Id) throw new BadRequestException($"{jobSchedulePutDTO.Id} was not found");

        var result = _mapper.Map<JobSchedule>(jobSchedulePutDTO);
        _repository.Update(result);
        await _repository.SaveAsync();
    }

    public async Task DeleteJobScheduleAsync(int id)
    {
        var schedules = _repository.GetAll().ToList();

        if (schedules.All(x => x.Id != id))
        {
            throw new NotFoundException("Not Found");
        }
        var schedule = await _repository.GetByIdAsync(id);
        _repository.Delete(schedule);
        await _repository.SaveAsync();
    }
}
