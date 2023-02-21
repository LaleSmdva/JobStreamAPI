﻿using JobStream.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.DTOs.CategoryDTO
{
    public class CategoriesPostDTO
    {
        //public int Id { get; set; }
        public string? Name { get; set; }
        //public ICollection<CategoryField>? CategoryField { get; set; } //numune: bank sahesi, marketinq
        public ICollection<Vacancy>? Vacancies { get; set; }
        public ICollection<CompanyAndCategory>? CompanyAndCategories { get; set; } //ok
    }
}