using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V=JobStream.Business.DTOs.VacanciesDTO;

namespace JobStream.Business.DTOs.JobScheduleDTO
{
    public class JobScheduleDTO
    {
        public int Id { get; set; }
        public string Schedule { get; set; }
        public ICollection<V.VacanciesDTO>? Vacancies { get; set; }
    }
}
