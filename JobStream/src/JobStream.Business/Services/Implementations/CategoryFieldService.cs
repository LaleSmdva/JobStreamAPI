using AutoMapper;
using JobStream.Business.DTOs.CategoryDTO;
using JobStream.Business.DTOs.CategoryFieldDTO;
using JobStream.Business.Exceptions;
using JobStream.Business.HelperServices.Interfaces;
using JobStream.Business.Services.Interfaces;
using JobStream.Core.Entities;
using JobStream.DataAccess.Repositories.Implementations;
using JobStream.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Services.Implementations
{
    public class CategoryFieldService : ICategoryFieldService
    {
        private readonly ICategoryFieldRepository _categoryFieldRepository;
        private readonly IMapper _mapper;

        public CategoryFieldService(ICategoryFieldRepository categoryFieldRepository, IMapper mapper)
        {
            _categoryFieldRepository = categoryFieldRepository;
            _mapper = mapper;
        }

        public async Task<List<CategoryFieldDTO>> GetAll()
        {
            var categories = await _categoryFieldRepository.GetAll().ToListAsync();
            var result = _mapper.Map<List<CategoryFieldDTO>>(categories);
            return result;
        }

        public async Task CreateCategoryFieldAsync(CategoryFieldPostDTO entity)
        {
            if (entity == null)
            {
                throw new BadRequestException("Category field data is missing");
            }
            var categories = _categoryFieldRepository.GetAll();

            if (categories.Any(c => c.Name == entity.Name))
            {
                throw new AlreadyExistsException("A category field with the same name already exists");
            }

            var categoryField = new CategoryField { Name = entity.Name, CategoryId=entity.CategoryId };

            await _categoryFieldRepository.CreateAsync(categoryField);
            await _categoryFieldRepository.SaveAsync();
        }

        public async Task UpdateCategoryFieldNameAsync(int id, CategoryFieldPutDTO entity)
        {
            var categoryFields = _categoryFieldRepository.GetByCondition(a => a.Id == entity.Id, false);
            if (categoryFields == null) throw new NotFoundException($"There is no category field with id: {id}");
            if (id != entity.Id) throw new BadRequestException($"{entity.Id} was not found");

            var result = _mapper.Map<CategoryField>(entity);
            _categoryFieldRepository.Update(result);
            await _categoryFieldRepository.SaveAsync();
        }

        public async Task DeleteCategoryFieldAsync(int id)
        {
            var categoryFields = _categoryFieldRepository.GetAll().ToList();

            if (categoryFields.All(x => x.Id != id))
            {
                throw new NotFoundException("Not Found");
            }
            var categoryField = await _categoryFieldRepository.GetByIdAsync(id);
            _categoryFieldRepository.Delete(categoryField);
            await _categoryFieldRepository.SaveAsync();
        }


    }
}
