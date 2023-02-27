using JobStream.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Core.Entities
{
    public class Applications:IEntity
    {
        public int Id { get; set; }
        public string? CV { get; set; }
        public int? VacancyId { get; set; }
        public Vacancy? Vacancy { get; set; }
        public int? CandidateResumeId { get; set; }
        public CandidateResume? CandidateResume { get; set; }
        public bool? IsAccepted { get; set; }

    }
}
