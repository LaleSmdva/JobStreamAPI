using JobStream.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.DTOs.VacanciesDTO
{
	public class VacanciesDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Location { get; set; }
		public string? JobLocation { get; set; } //remote/hybrid remote/ on the road/in person(precise location)/in person(general location)
		public int Salary { get; set; }
		public string Requirements { get; set; }
		public string Description { get; set; }
		public int ExperienceLevel { get; set; }
		public string? HREmail { get; set; }
		public DateTime? PostedOn { get; set; }  //  ???

		public int CompanyId { get; set; }
		public string CompanyName { get; set; }
		public int JobTypeId { get; set; }
		//public JobType JobType { get; set; } //full-time,part-time,temporary,permanent,internship,contract,etc
		public int JobScheduleId { get; set; } //8 hour shift ,day shift,evening shift, self-determined schedule,etc
		//public JobSchedule JobSchedule { get; set; }

		public int CategoryId { get; set; }
		//public Category Category { get; set; }
		//yazmamisam
		public DateTime ClosingDate { get; set; }
		public string? OfferedBenfits { get; set; } //health insurance,paid time-off,flexible schedule
	}
}
