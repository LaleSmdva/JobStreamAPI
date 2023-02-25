using JobStream.Core.Entities.Identity;
using JobStream.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Core.Entities
{
	public class Company : IEntity
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Location { get; set; }
		public string? EmailForCv { get; set; }
		public string? AboutCompany { get; set; }
		public int? NumberOfEmployees { get; set; }
		//new 26
		public string? AppUserId { get; set; }
		public AppUser? AppUser { get; set; }

		public string? Email { get; set; }
		public string? Logo { get; set; }
		public DateTime? IncorporationDate { get; set; }
		public string? Telephone { get; set; }
		public bool? IsDeleted { get; set; }
		public ICollection<Vacancy>? Vacancies { get; set; }
		public ICollection<CompanyAndCategory>? CompanyAndCategories { get; set; }
	}
}
