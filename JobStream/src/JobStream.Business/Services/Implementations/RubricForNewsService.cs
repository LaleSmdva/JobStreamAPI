using AutoMapper;
using JobStream.Business.DTOs.NewsDTO;
using JobStream.Business.DTOs.RubricForNewsDTO;
using JobStream.Business.Exceptions;
using JobStream.Business.HelperServices.Implementations;
using JobStream.Business.HelperServices.Interfaces;
using JobStream.Business.Services.Interfaces;
using JobStream.Core.Entities;
using JobStream.DataAccess.Repositories.Implementations;
using JobStream.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    

namespace JobStream.Business.Services.Implementations
{

    public class RubricForNewsService : IRubricForNewsService
    {

        private readonly IRubricForNewsRepository _rubricForNewsRepository;
        private readonly IMapper _mapper;

        public RubricForNewsService(IRubricForNewsRepository rubricForNewsRepository, IMapper mapper)
        {
            _rubricForNewsRepository = rubricForNewsRepository;
            _mapper = mapper;
        }


        public async Task<List<RubricForNewsDTO>> GetAllAsync()
        {
            var rubricForNews = await _rubricForNewsRepository.GetAll()
                .Include(a=>a.News).ToListAsync();
            var list = _mapper.Map<List<RubricForNewsDTO>>(rubricForNews);
            return list;
        }

        public async Task CreateRubricForNewsAsync(RubricForNewsPostDTO entity)
        {
            if (entity == null)
            {
                throw new NullReferenceException("Rubric for news can't be null");
            }
            if (await _rubricForNewsRepository.GetAll().AnyAsync(r => r.Name == entity.Name)) throw new AlreadyExistsException("Rubric with that name already exists");
            var rubric = _mapper.Map<RubricForNews>(entity);
            await _rubricForNewsRepository.CreateAsync(rubric);
            await _rubricForNewsRepository.SaveAsync();
        }

        public async Task UpdateRubricForNewsAsync(int id, RubricForNewsPutDTO rubric)
        {
            if (rubric == null)
            {
                throw new NullReferenceException("Rubric for news can't be null");
            }
            var rubricForNews = _rubricForNewsRepository.GetByCondition(a => a.Id == rubric.Id, false);
            if (rubricForNews == null) throw new NotFoundException($"There is no rubric with id: {id}");
            if (id != rubric.Id) throw new BadRequestException("Id's do not match");

            var result = _mapper.Map<RubricForNews>(rubric);
            _rubricForNewsRepository.Update(result);
            await _rubricForNewsRepository.SaveAsync();
        }

        public async Task DeleteRubricForNewsAsync(int id)
        {
            var rubricForNews = _rubricForNewsRepository.GetAll().ToList();

            if (rubricForNews.All(x => x.Id != id))
            {
                throw new NotFoundException("Not Found");
            }
            var result = await _rubricForNewsRepository.GetByIdAsync(id);
            _rubricForNewsRepository.Delete(result);
            await _rubricForNewsRepository.SaveAsync();
        }

    
    }
}
