using AutoMapper;
using JobStream.Business.DTOs.RubricForArticlesDTO;
using JobStream.Business.DTOs.RubricForNewsDTO;
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

namespace JobStream.Business.Services.Implementations
{
    public class RubricForArticlesService:IRubricForArticlesService
    {
        private readonly IRubricForArticlesRepository _rubricForArticlesRepository;
        private readonly IMapper _mapper;

        public RubricForArticlesService(IRubricForArticlesRepository rubricForArticlesRepository, IMapper mapper)
        {
            _rubricForArticlesRepository = rubricForArticlesRepository;
            _mapper = mapper;
        }

        public async Task<List<RubricForArticlesDTO>> GetAllAsync()
        {
             var rubricForArticles = await _rubricForArticlesRepository.GetAll().ToListAsync();
            var list = _mapper.Map<List<RubricForArticlesDTO>>(rubricForArticles);
            return list;
        }

        public async Task CreateRubricForArticlesAsync(RubricForArticlesPostDTO entity)
        {
            if (entity == null)
            {
                throw new NullReferenceException("Rubric for article can't be null");
            }
            if (await _rubricForArticlesRepository.GetAll().AnyAsync(r => r.Name == entity.Name)) throw new AlreadyExistsException("Rubric with that name already exists");
   
            var rubric = _mapper.Map<RubricForArticles>(entity);
            await _rubricForArticlesRepository.CreateAsync(rubric);
            await _rubricForArticlesRepository.SaveAsync();
        }

        public async Task UpdateRubricForArticlesAsync(int id, RubricForArticlesPutDTO rubric)
        {
            if (rubric == null)
            {
                throw new NullReferenceException("Rubric for articles can't be null");
            }
            var rubricForArticles = _rubricForArticlesRepository.GetByCondition(a => a.Id == rubric.Id, false);
            if (rubricForArticles == null) throw new NotFoundException($"There is no rubric with id: {id}");
            if (id != rubric.Id) throw new BadRequestException($"{rubric.Id} was not found");

            var result = _mapper.Map<RubricForArticles>(rubric);
            _rubricForArticlesRepository.Update(result);
            await _rubricForArticlesRepository.SaveAsync();
        }

        public async Task DeleteRubricForArticlesAsync(int id)
        {
            var rubricForArticles = _rubricForArticlesRepository.GetAll().ToList();

            if (rubricForArticles.All(x => x.Id != id))
            {
                throw new NotFoundException("Not Found");
            }
            var result = await _rubricForArticlesRepository.GetByIdAsync(id);
            _rubricForArticlesRepository.Delete(result);
            await _rubricForArticlesRepository.SaveAsync();
        }

    
    }
}
