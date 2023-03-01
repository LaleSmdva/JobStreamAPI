using JobStream.Business.DTOs.VacanciesDTO;
using JobStream.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V = JobStream.Business.DTOs.VacanciesDTO;

namespace JobStream.Business.DTOs.CategoryDTO
{
    public class CategoriesPostDTO
    {
        public string? Name { get; set; }
        //public ICollection<CategoryField>? CategoryField { get; set; } //numune: bank sahesi, marketinq
        //public ICollection<VacancyCategoryPostDTO>? Vacancies { get; set; }
        //public ICollection<CompanyAndCategory>? CompanyAndCategories { get; set; } //ok
    }
}
