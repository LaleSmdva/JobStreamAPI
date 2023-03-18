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
        private readonly IAboutUsRepository _aboutUsRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _environment;
        public AboutUsService(IAboutUsRepository repository, IMapper mapper, IFileService fileService, IWebHostEnvironment environment)
        {
            _aboutUsRepository = repository;
            _mapper = mapper;
            _fileService = fileService;
            _environment = environment;
        }


        public async Task<List<AboutUsDTO>> GetAboutUsAsync()
        {
            var aboutUs = await _aboutUsRepository.GetAll().ToListAsync();
            var result = _mapper.Map<List<AboutUsDTO>>(aboutUs);
            return result;
        }

        public async Task CreateAboutUsAsync(AboutUsPostDTO entity)
        {
            var about = _aboutUsRepository.GetAll().FirstOrDefault(/*a => a.Email == entity.Email*/);
            if (about!=null)
            {
                throw new AlreadyExistsException("About us already created");
            }
            if (entity.Image != null)
            {
                var fileName = await _fileService.CopyFileAsync(entity.Image, _environment.WebRootPath, "images", "AboutUs");
                if (entity == null) throw new NullReferenceException("Entity can't ne null");
                var aboutUs = _mapper.Map<AboutUs>(entity);
                aboutUs.Image = fileName;
                await _aboutUsRepository.CreateAsync(aboutUs);
            }
            await _aboutUsRepository.SaveAsync();

        }

        public async Task DeleteAboutUsAsync(int id)
        {
            var aboutUs = _aboutUsRepository.GetAll();
            if (aboutUs.All(x => x.Id != id))
            {
                throw new NotFoundException("Not Found");
            }
            var about = await _aboutUsRepository.GetByIdAsync(id);
            var result = _mapper.Map<AboutUs>(about);

            await _fileService.DeleteFileAsync(about.Image, _environment.WebRootPath, "images", "AboutUs");
            _aboutUsRepository.Delete(result);
            await _aboutUsRepository.SaveAsync();
        }

        public async Task UpdateAboutUsAsync(int id, AboutUsPutDTO aboutUs)
        {
            var about = _aboutUsRepository.GetByCondition(a => a.Id == aboutUs.Id, false);
            if (about == null) throw new NotFoundException($"There is no data with id: {id}");
            if (id != aboutUs.Id) throw new BadRequestException("Id's don't match");
            if (aboutUs.Image != null)
            {
                if (aboutUs == null) throw new NullReferenceException("Entity can't ne null");
                var fileName = await _fileService.CopyFileAsync(aboutUs.Image, _environment.WebRootPath, "images", "AboutUs");
                var abUs = _mapper.Map<AboutUs>(aboutUs);
                abUs.Image = fileName;
                _aboutUsRepository.Update(abUs);
            }
            await _aboutUsRepository.SaveAsync();
        }
    }
}
