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
        List<VacanciesDTO> GetVacanciesByCategory(Expression<Func<Vacancy, bool>> expression);
        Task CreateVacancyAsync(VacanciesPostDTO entity);
        Task UpdateVacancyAsync(int id, VacanciesPutDTO company);
        Task DeleteVacancyAsync(int id);
        IEnumerable<Vacancy> GetExpiredVacancies();
        Task VacancyCleanUp();

    }
}
