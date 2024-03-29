﻿using JobStream.Business.DTOs.VacanciesDTO;
using JobStream.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V = JobStream.Business.DTOs.VacanciesDTO;
using C = JobStream.Business.DTOs.CompanyAndCategoryDTO;

namespace JobStream.Business.DTOs.CompanyDTO
{
	public class CompanyDTO
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Location { get; set; }
		public string? EmailForCv { get; set; }
		public string? AboutCompany { get; set; }
		public int? NumberOfEmployees { get; set; }
		//public int AppUserId { get; set; }
		//public AppUser AppUser { get; set; }

		public string? Email { get; set; }
		public string? Logo { get; set; }
		public DateTime? IncorporationDate { get; set; }
		public string? Telephone { get; set; }
		//public bool? IsDeleted { get; set; }
		public ICollection<V.VacanciesDTO>? Vacancies { get; set; }
		public ICollection<C.CompanyAndCategoryDTO>? CompanyAndCategories { get; set; }
	}
}
