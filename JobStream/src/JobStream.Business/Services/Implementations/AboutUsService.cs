using AutoMapper;
using JobStream.Business.DTOs.AboutUsDTO;
using JobStream.Business.Exceptions;
using JobStream.Business.HelperServices.Interfaces;
using JobStream.Business.Services.Interfaces;
using JobStream.Core.Entities;
using JobStream.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace JobStream.Business.Services.Implementations
{
    public class AboutUsService : IAboutUsService
    {
        private readonly IAboutUsRepository _repository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _environment;
        public AboutUsService(IAboutUsRepository repository, IMapper mapper, IFileService fileService, IWebHostEnvironment environment)
        {
            _repository = repository;
            _mapper = mapper;
            _fileService = fileService;
            _environment = environment;
        }


        public async Task<List<AboutUsDTO>> GetAboutUsAsync()
        {
            var aboutUs = await _repository.GetAll().ToListAsync();
            var result = _mapper.Map<List<AboutUsDTO>>(aboutUs);
            return result;
        }

        public async Task CreateAboutUsAsync(AboutUsPostDTO entity)
        {
            if (entity.Image != null)
            {
                var fileName = await _fileService.CopyFileAsync(entity.Image, _environment.WebRootPath, "images", "AboutUs");
                if (entity == null) throw new NullReferenceException("Entity can't ne null");
                var aboutUs = _mapper.Map<AboutUs>(entity);
                aboutUs.Image = fileName;
                await _repository.CreateAsync(aboutUs);
            }
            await _repository.SaveAsync();
        }

        public async Task DeleteAboutUsAsync(int id)
        {
            var aboutUs = _repository.GetAll();
            if (aboutUs.All(x => x.Id != id))
            {
                throw new NotFoundException("Not Found");
            }
            var about = await _repository.GetByIdAsync(id);
            var result = _mapper.Map<AboutUs>(about);
     
            await _fileService.DeleteFileAsync(about.Image, _environment.WebRootPath, "images", "AboutUs");
            _repository.Delete(result);
            await _repository.SaveAsync();
        }

        public async Task UpdateAboutUsAsync(int id, AboutUsPutDTO aboutUs)
        {
            var about = _repository.GetByCondition(a => a.Id == aboutUs.Id, false);
            if (about == null) throw new NotFoundException($"There is no data with id: {id}");
            if (id != aboutUs.Id) throw new BadRequestException("Id's don't match");

            var result = _mapper.Map<AboutUs>(aboutUs);
            _repository.Update(result);
            await _repository.SaveAsync();
        }
    }
}
