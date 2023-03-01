using JobStream.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V = JobStream.Business.DTOs.VacanciesDTO;
namespace JobStream.Business.DTOs.CompanyDTO
{
	public class CompanyPutDTO
	{
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        public string? EmailForCv { get; set; }
        public string? AboutCompany { get; set; }
        public int? NumberOfEmployees { get; set; }
        //public int AppUserId { get; set; }
        //public AppUser AppUser { get; set; }

        public string? Email { get; set; }
        public IFormFile? Logo { get; set; }
        public string? Telephone { get; set; }
    }
}
