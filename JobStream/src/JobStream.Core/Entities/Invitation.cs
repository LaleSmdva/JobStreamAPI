using JobStream.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Core.Entities
{
    public class Invitation:IEntity
    {
        public int Id { get; set; }
        public DateTime InterviewDate { get; set; }
        //public Company Company { get; set; }
        //public Vacancy Vacancy { get; set; }
        //public List<int> CandidateIds { get; set; }
        public string InterviewLocation { get; set; }
        public string Message { get; set; }
    }
}
