using AutoMapper;
using JobStream.Business.DTOs.CompanyDTO;
using JobStream.Business.Exceptions;
using JobStream.Business.HelperServices.Interfaces;
using JobStream.Business.Mappers;
using JobStream.Business.Services.Interfaces;
using JobStream.Business.Utilities;
using JobStream.Core.Entities;
using JobStream.Core.Interfaces;
using JobStream.DataAccess.Contexts;
using JobStream.DataAccess.Repositories.Implementations;
using JobStream.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace JobStream.Business.Services.Implementations
{

    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _repository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private readonly IFileService _fileService;
        private readonly IVacanciesRepository _vacanciesRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICompanyAndCategoryRepository _companyAndCategoryRepository;
        private readonly AppDbContext _context;

        public CompanyService(ICompanyRepository repository, IMapper mapper, IWebHostEnvironment environment, IFileService fileService, IVacanciesRepository vacanciesRepository, ICategoryRepository categoryRepository, ICompanyAndCategoryRepository companyAndCategoryRepository, AppDbContext context)
        {
            _repository = repository;
            _mapper = mapper;
            _environment = environment;
            _fileService = fileService;
            _vacanciesRepository = vacanciesRepository;
            _categoryRepository = categoryRepository;
            _companyAndCategoryRepository = companyAndCategoryRepository;
            _context = context;
        }


        ///AddCategory
        ///categoryname,
        ///AddVacancy
        ///ContactApplicant

        public async Task<List<CompanyDTO>> GetAllAsync()
        {
            var companies = await _repository.GetAll().Include(v => v.Vacancies)
                .Include(x => x.CompanyAndCategories).ThenInclude(cc => cc.Category).ToListAsync();
            var result = _mapper.Map<List<CompanyDTO>>(companies);
            return result;
        }



        ///one to  many? many to  many
        //      public async Task AddCategory(int companyId,int categoryId)
        //{
        //	var company=await _repository.GetByIdAsync(companyId);
        //	var category=await _categoryRepository.GetByIdAsync(categoryId);

        //	company.CompanyAndCategories.Add();
        //}



        /// one to many

        //     public async Task AddVacancy(int vacancyId, int companyId)
        //     {
        //         var company = await _repository.GetByIdAsync(companyId);
        //         var vacancy = await _vacanciesRepository.GetByIdAsync(vacancyId);

        //company.Vacancies.Add(vacancy);
        //     }

        public List<CompanyDTO> GetByCondition(Expression<Func<Company, bool>> expression)
        {

            var company = _repository.GetByCondition(expression).ToList();
            var result = _mapper.Map<List<CompanyDTO>>(company);
            return result;
        }

        public async Task<CompanyDTO> GetByIdAsync(int id)
        {
            var companies = await _repository.GetAll()
                .Include(v => v.Vacancies)
                .Include(x => x.CompanyAndCategories).ThenInclude(cc => cc.Category).ToListAsync();
            if (companies.All(x => x.Id != id))
            {
                throw new NotFoundException("Not Found");
            }
            var company = await _repository.GetByIdAsync(id);
            var result = _mapper.Map<CompanyDTO>(company);
            return result;
        }

        public async Task CreateAsync(CompanyPostDTO entity)
        {
            //await entity.Logo.CopyFileAsync(_environment.WebRootPath, "images", "companyLogos");
            await _fileService.CopyFileAsync(entity.Logo, _environment.WebRootPath, "images", "companyLogos");
            var company = _mapper.Map<Company>(entity);

            if (entity.CatagoriesId == null) throw new NullReferenceException("Category can not be null");
            foreach (var catId in entity.CatagoriesId)
            {
                var category = await _categoryRepository.GetByIdAsync(catId);
                if (category == null) throw new NotFoundException("Not found");
            }
            await _repository.CreateAsync(company);
            await _repository.SaveAsync();

            List<CompanyAndCategory> companyAndCategoryList = new();
            foreach (var catID in entity.CatagoriesId)
            {
                CompanyAndCategory companyAndCategory = new();
                companyAndCategory.CategoryId = catID;
                companyAndCategory.CompanyId = company.Id;
                companyAndCategoryList.Add(companyAndCategory);

            }
            foreach (var item in companyAndCategoryList)
            {
                await _companyAndCategoryRepository.CreateAsync(item);
            }
            await _companyAndCategoryRepository.SaveAsync();

            //List<Vacancy> vacancies = new();
            //foreach (var topicId in entity.Vacancies)
            //{
            //    vacancies.Add(new()
            //    {
            //        CompanyId = companies.Id,
            //        JobTypeId = companies.Id
            //    });
            //}
            //companies.Vacancies = vacancies;



            //_companyAndCategoryRepository.CreateAsync(companyAndCatagories.)


            ///////////////////////////////////
            //List<Vacancy> vacancies = new();
            //foreach (var topicId in entity.Vacancies)
            //{
            //    vacancies.Add(new()
            //    {
            //        CompanyId = companies.Id,
            //        JobTypeId = companies.Id
            //    });
            //}
            //companies.Vacancies = vacancies;

            //////////////////////////////
            ///
            //List<CompanyAndCategory> companyAndCategories= new();
            //foreach (var item in entity.CompanyAndCategories)
            //{
            //	companyAndCategories.Add(new()
            //	{
            //		CompanyId = companies.Id,
            //		CategoryId=companies.Id
            //	});
            //}

            //companies.CompanyAndCategories= companyAndCategories;

        }


        public async Task Update(int id, List<int> addedCategoryId, List<int> deletedCategoryId, CompanyPutDTO companyPutDTO)
        {
            //var Company = _repository.GetByCondition(c => c.Id == company.Id, false);
            //if (Company == null) throw new NotFoundException("Not Found");
            //if (id != company.Id) throw new BadRequestException("Id is not valid");
            //var result = _mapper.Map<Company>(company);
            //_repository.Update(result);
            //await _repository.SaveAsync();

            //var Company = _repository.GetByCondition(c => c.Id == company.Id, false);
            //if (Company == null) throw new NotFoundException("Not Found");
            //if (companyId != company.Id) throw new BadRequestException("Id is not valid");


            /// Updating Company
            var companies = _repository.GetByCondition(a => a.Id == companyPutDTO.Id, false);
            if (companies == null) throw new NotFoundException($"There is no company with id: {id}");
            if (id != companyPutDTO.Id) throw new BadRequestException($"{companyPutDTO.Id} was not found");


            var result = _mapper.Map<Company>(companyPutDTO);
            _repository.Update(result);
            await _repository.SaveAsync();

            //add
            List<CompanyAndCategory> companyAndCategoryList = new();
            foreach (var catID in addedCategoryId)
            {
                CompanyAndCategory companyAndCategory = new();
                companyAndCategory.CategoryId = catID;
                companyAndCategory.CompanyId = result.Id;
                companyAndCategoryList.Add(companyAndCategory);

            }
            foreach (var item in companyAndCategoryList)
            {
                await _companyAndCategoryRepository.CreateAsync(item);
            }
            await _companyAndCategoryRepository.SaveAsync();


            //delete
            foreach (var categoryId in deletedCategoryId)
            {
                var companyAndCategory = await _companyAndCategoryRepository
                    .GetByCondition(cac => cac.CategoryId == categoryId && cac.CompanyId == result.Id, true)
                    .FirstOrDefaultAsync();

                if (companyAndCategory != null)
                {
                    _companyAndCategoryRepository.Delete(companyAndCategory);
                }
            }
            await _companyAndCategoryRepository.SaveAsync();
            //List<CompanyAndCategory> companyAndCategoryList2 = new List<CompanyAndCategory>();
            //foreach (var categoryId in deletedCategoryId)
            //{
            //    var companyAndCategory = new CompanyAndCategory
            //    {
            //        CategoryId = categoryId,
            //        CompanyId = result.Id,
            //    };
            //    companyAndCategoryList2.Add(companyAndCategory);
            //}

            //foreach (var item in companyAndCategoryList2)
            //{
            //    _companyAndCategoryRepository.Delete(item);
            //}
            //await _companyAndCategoryRepository.SaveAsync();


            ///Updating Vacancy for that Company
            //    var company = _repository.GetAll().Include(c => c.Vacancies)
            //.FirstOrDefault(c => c.Id == id);

            //    //    if (company == null)
            //    //    {
            //    //        throw new NotFoundException("");
            //    //    }

            //    var vacancy = company.Vacancies.FirstOrDefault(v => v.Id == companyPutDTO.vacancyId);

            //    if (vacancy == null)
            //    {
            //        throw new NotFoundException("");
            //    }

            //    //vacancy.CompanyId = id;
            //    var ress = _mapper.Map<CompanyPutDTO>(company);
            //    ress.vacancyId = vacancyId;
            //    await _context.SaveChangesAsync();


            //company.vacancyId = vacancyId;
            //var resultt = _mapper.Map<Company>(company);
            //_context.Update(resultt);
            //await _context.SaveChangesAsync();



            ///////////////////////////////////////

            //var res=_mapper.Map<Company>(company);
            //var vacancy = await _repository.GetByIdAsync(res.Id);
            //if (vacancy == null) throw new NotFoundException("Not found");
            //else
            //{
            //    var res = _mapper.Map<Company>(vacId);
            //    _repository.Update(res);
            //}

            //List<Company> companies = _repository.GetAll().ToList();
            //var list = _mapper.Map<List<CompanyPutDTO>>(companies);
            //if (vacancyId != Idd) continue;
            //}
            //

            //
            //var result = _mapper.Map<Company>(company);
            //_repository.Update(result);

            //await _repository.SaveAsync();
        }

        public async Task Delete(int id)
        {
            var companies = _repository.GetAll();
            if (companies.All(x => x.Id != id))
            {
                throw new NotFoundException("Not Found");
            }
            var company = await _repository.GetByIdAsync(id);
            var result = _mapper.Map<Company>(company);
            _repository.Delete(result);
            await _repository.SaveAsync();
        }

        public async Task DeleteVacancy(int id, int vacancyId)
        {
            //var companies = _repository.GetAll();
            //if (companies.All(x => x.Id != id))
            //{
            //    throw new NotFoundException("Not Found");
            //}

            //var company = await _repository.GetByIdAsync(id);
            //var result = _mapper.Map<Company>(company);
            //_repository.Delete(result);
            //await _repository.SaveAsync();

            var company = _repository.GetAll().Include(c => c.Vacancies)
       .FirstOrDefault(c => c.Id == id);

            if (company == null)
            {
                throw new NotFoundException("Company not found");
            }

            var vacancy = _vacanciesRepository.GetAll().FirstOrDefault(v => v.Id == vacancyId);
            if (vacancy == null)
            {
                throw new NotFoundException("Vacancy not found");
            }

            company.Vacancies.Remove(vacancy);
            _context.SaveChanges();
        }
    }
}
