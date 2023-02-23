using JobStream.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C = JobStream.Business.DTOs.CategoryFieldDTO;
using V = JobStream.Business.DTOs.VacanciesDTO;
using CC = JobStream.Business.DTOs.CompanyAndCategoryDTO;

namespace JobStream.Business.DTOs.CategoryDTO
{
    public class CategoriesDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<C.CategoryFieldDTO>? CategoryField { get; set; } //numune: bank sahesi, marketinq
        public ICollection<V.VacanciesDTO>? Vacancies { get; set; }
        public ICollection<CC.CompanyAndCategoryDTO>? CompanyAndCategories { get; set; } //ok
    }
}
