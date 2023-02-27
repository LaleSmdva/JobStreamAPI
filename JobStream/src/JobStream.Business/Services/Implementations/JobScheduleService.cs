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
        if (!jobSchedules.Any()) throw new NotFoundException("No job schedule was found");
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
        var jobSchedule = await _repository.GetAll().SingleOrDefaultAsync(js => js.Id == id);
        if (jobSchedule == null)
            throw new NotFoundException($"JobSchedule with ID {id} not found");

        var vacancies = await _vacanciesRepository.GetAll()
            .Where(v => v.JobScheduleId == id)
            .ToListAsync();

        if (vacancies.Count == 0)
            throw new NotFoundException($"No vacancies found for JobSchedule with ID {id}");

        var result = _mapper.Map<List<VacanciesDTO>>(vacancies);
        return result;
    }


    public async Task CreateJobScheduleAsync(JobSchedulePostDTO entity)
    {
        if (entity == null) throw new NullReferenceException("Schedule name can't ne null");
        if (await _repository.GetAll().AnyAsync(j => j.Schedule == entity.Schedule))
            throw new AlreadyExistsException("Job schedule with that name already exists");
        var jobSchedule = _mapper.Map<JobSchedule>(entity);
        await _repository.CreateAsync(jobSchedule);
        await _repository.SaveAsync();
    }

    public async Task UpdateJobScheduleAsync(int id, JobSchedulePutDTO jobSchedulePutDTO)
    {
        if (id != jobSchedulePutDTO.Id)
            throw new BadRequestException($"{jobSchedulePutDTO.Id} was not found");
        var schedule = _repository.GetAll().FirstOrDefault(a => a.Id == jobSchedulePutDTO.Id);
        if (schedule == null) throw new NotFoundException("There is no schedule with that id ");

        var result = _mapper.Map<JobSchedule>(jobSchedulePutDTO);
        _repository.Update(result);
        await _repository.SaveAsync();
    }

    public async Task DeleteJobScheduleAsync(int id)
    {
        var schedule = await _repository.GetByIdAsync(id);
        if(schedule == null) throw new NotFoundException("Not Found");
        _repository.Delete(schedule);
        await _repository.SaveAsync();
    }
}
