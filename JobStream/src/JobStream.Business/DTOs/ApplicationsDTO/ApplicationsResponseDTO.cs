using JobStream.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.DTOs.ApplicationsDTO
{
    public class ApplicationsResponseDTO
    {
        public string? CV { get; set; }
        public int? VacancyId { get; set; }
        public bool? IsAccepted { get; set; }
    }
}
