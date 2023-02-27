using JobStream.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.DTOs.ApplicationsDTO
{
    public class ApplicationsPostDTO
    {
        //public int Id { get; set; }
        public string? CV { get; set; }
        public int? VacancyId { get; set; }
        //public Vacancy? Vacancy { get; set; }
        public int? CandidateResumeId { get; set; }
        //public CandidateResume? CandidateResume { get; set; }
    }
}
