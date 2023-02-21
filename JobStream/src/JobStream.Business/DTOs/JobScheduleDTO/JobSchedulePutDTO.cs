using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.DTOs.JobScheduleDTO
{
    public class JobSchedulePutDTO
    {
        public int Id { get; set; }
        public string Schedule { get; set; }
        //public ICollection<Vacancy>? Vacancies { get; set; }
    }
}
