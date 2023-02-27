using AutoMapper;
using JobStream.Business.DTOs.ArticleDTO;
using JobStream.Business.DTOs.NewsDTO;
using JobStream.Business.Exceptions;
using JobStream.Business.HelperServices.Interfaces;
using JobStream.Business.Services.Interfaces;
using JobStream.Core.Entities;
using JobStream.Core.Interfaces;
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
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private readonly IFileService _fileService;
        private readonly IRubricForNewsRepository _rubricForNewsRepository;

        public NewsService(INewsRepository newsRepository, IMapper mapper, IWebHostEnvironment environment, IFileService fileService, IRubricForNewsRepository rubricForNewsRepository)
        {
            _newsRepository = newsRepository;
            _mapper = mapper;
            _environment = environment;
            _fileService = fileService;
            _rubricForNewsRepository = rubricForNewsRepository;
        }

        public async Task<List<NewsDTO>> GetAllAsync()
        {
            var news = await _newsRepository.GetAll().ToListAsync();
            var list = _mapper.Map<List<NewsDTO>>(news);
            return list;
        }

        public List<NewsDTO> GetNewsByTitle(string title/*Expression<Func<Article, bool>> expression*/)
        {
            var news = _newsRepository.GetAll().Where(a => a.Title.Contains(title)).ToList();
            if (news is null) throw new NotFoundException($"No title with name {title} found");
            var list = _mapper.Map<List<NewsDTO>>(news);
            return list;
        }

        public async Task<NewsDTO> GetNewsByIdAsync(int id)
        {
            var news = await _newsRepository.GetByIdAsync(id);
            if (news == null) throw new NotFoundException("No news found");
            var result = _mapper.Map<NewsDTO>(news);
            return result;
        }


        public async Task<List<NewsDTO>> GetNewsByRubricId(int id)
        {
            var rubricForNews = await _rubricForNewsRepository.GetByIdAsync(id);
            if (rubricForNews == null) throw new NotFoundException("Not found");
            var news = await _newsRepository.GetAll()
                .Where(x => x.RubricForNewsId == id).ToListAsync();
            if (news == null || news.Count() == 0) throw new NotFoundException("News not found");
            var result = _mapper.Map<List<NewsDTO>>(news);
            return result;
        }

        public async Task CreateNewsAsync(NewsPostDTO entity)
        {
            if (entity == null)
            {
                throw new NullReferenceException("News can't be null");
            }
            if (await _rubricForNewsRepository.GetAll().AllAsync(r => r.Id != entity.RubricForNewsId)) throw new NotFoundException("There is no rubric with that id");
    
            var fileName = await _fileService.CopyFileAsync(entity.Image, _environment.WebRootPath, "images", "News");
        
            if (entity == null) throw new NullReferenceException("News can't ne null");
            var news = _mapper.Map<News>(entity);
            news.PostedOn = DateTime.Now;
            news.Image = fileName;
            await _newsRepository.CreateAsync(news);
            await _newsRepository.SaveAsync();
        }


        public async Task UpdateNewsAsync(int id, NewsPutDTO news)
        {
            if (news == null)
            {
                throw new NullReferenceException("News can't be null");
            }
            var articles = _newsRepository.GetByCondition(a => a.Id == news.Id, false);
            if (articles == null) throw new NotFoundException($"There is no news with id: {id}");
            if (id != news.Id) throw new BadRequestException($"{news.Id} was not found");

            var fileName = await _fileService.CopyFileAsync(news.Image, _environment.WebRootPath, "images", "News");
            var result = _mapper.Map<News>(news);
            result.Image = fileName;
            _newsRepository.Update(result);
            await _newsRepository.SaveAsync();
        }
        public async Task DeleteNewsAsync(int id)
        {
            var news = _newsRepository.GetAll().ToList();

            if (news.All(x => x.Id != id))
            {
                throw new NotFoundException("Not Found");
            }
            var deletedNews = await _newsRepository.GetByIdAsync(id);
            await _fileService.DeleteFileAsync(deletedNews.Image, _environment.WebRootPath, "images", "News");
            _newsRepository.Delete(deletedNews);
            await _newsRepository.SaveAsync();


        }
    }
}
