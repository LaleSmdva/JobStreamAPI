using AutoMapper;
using JobStream.Business.DTOs.CategoryFieldDTO;
using JobStream.Business.Exceptions;
using JobStream.Business.Services.Interfaces;
using JobStream.Core.Entities;
using JobStream.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobStream.Business.Services.Implementations
{
    public class CategoryFieldService : ICategoryFieldService
    {
        private readonly ICategoryFieldRepository _categoryFieldRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryFieldService(ICategoryFieldRepository categoryFieldRepository, IMapper mapper, ICategoryRepository categoryRepository)
        {
            _categoryFieldRepository = categoryFieldRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
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

            var checkForCategory = await _categoryRepository.GetAll().Where(cf => cf.Id == entity.CategoryId).ToListAsync();
            if (checkForCategory.Count() == 0)
            {
                throw new BadRequestException($"There is no category with id: {entity.CategoryId}");
            }
            var categoryField = new CategoryField { Name = entity.Name, CategoryId = entity.CategoryId };

            await _categoryFieldRepository.CreateAsync(categoryField);
            await _categoryFieldRepository.SaveAsync();
        }

        public async Task UpdateCategoryFieldNameAsync(int id, CategoryFieldPutDTO entity)
        {
            if (id != entity.Id) throw new BadRequestException($"Category field id: {entity.Id} was not found");
            var checkForCategory = await _categoryRepository.GetAll().Where(cf => cf.Id == entity.CategoryId).ToListAsync();
            if (checkForCategory.Count() == 0)
            {
                throw new BadRequestException($"There is no category with id: {entity.CategoryId}");
            }
            var fieldExistsInCategory = await _categoryFieldRepository.GetAll().Where(c => c.Id == id).Where(c => c.CategoryId == entity.CategoryId).ToListAsync();
            if (fieldExistsInCategory.Count() == 0)
            {
                throw new BadRequestException($"There is no category field with id: {id} in category with id: {entity.CategoryId}");
            }
            //var result = _mapper.Map<CategoryField>(entity);
            foreach (var categoryField in fieldExistsInCategory)
            {
                categoryField.CategoryId = entity.CategoryId;
                categoryField.Name = entity.Name;
                _categoryFieldRepository.Update(categoryField);
            }
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
