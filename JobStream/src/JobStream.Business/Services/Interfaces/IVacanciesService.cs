using JobStream.Business.DTOs.CompanyDTO;
using JobStream.Business.DTOs.VacanciesDTO;
using JobStream.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Services.Interfaces
{
    public interface IVacanciesService
    {
        List<VacanciesDTO> GetAll();
        Task<VacanciesDTO> GetVacancyByIdAsync(int id);
        Task<List<VacanciesDTO>> GetVacanciesByCategoryAsync(List<int> categoryIds);
        Task<List<VacanciesDTO>> SearchVacancies(string? keyword, string? location, List<int>? categoryId, string? companyName);
        IEnumerable<Vacancy> GetExpiredVacancies();
        Task VacancyCleanUp();

    }
}
