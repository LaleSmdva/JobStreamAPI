using JobStream.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.DTOs.JobTypeDTO
{
    public class JobTypePutDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public ICollection<Vacancy>? Vacancies { get; set; }
    }
}
