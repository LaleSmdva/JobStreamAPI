using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.DTOs.VacanciesDTO
{
    public class VacancyCategoryPostDTO
    {
        //public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string? JobLocation { get; set; } 
        public int Salary { get; set; }
        public string Requirements { get; set; }
        public string Description { get; set; }
        public string ExperienceLevel { get; set; }
        public string? HREmail { get; set; }
        //public DateTime? PostedOn { get; set; }  

        //public int CompanyId { get; set; }
        //public string CompanyName { get; set; }
        //public Company Company { get; set; }
        public int JobTypeId { get; set; }
        //public JobType JobType { get; set; } 
        public int JobScheduleId { get; set; } 
        //public JobSchedule? JobSchedule { get; set; }

        //public int CategoryId { get; set; }
        //public Category Category { get; set; }
        //yazmamisam
        //public DateTime ClosingDate { get; set; }
        public string? OfferedBenfits { get; set; } 
    }
}
