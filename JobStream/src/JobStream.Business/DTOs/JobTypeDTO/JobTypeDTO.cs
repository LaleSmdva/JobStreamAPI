using JobStream.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V=JobStream.Business.DTOs.VacanciesDTO;

namespace JobStream.Business.DTOs.JobTypeDTO
{
    public class JobTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<V.VacanciesDTO>? Vacancies { get; set; }
    }
}
