using JobStream.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C=JobStream.Core.Entities;

namespace JobStream.Business.Validators.InvitationDTO
{
    public class InvitationDTO
    {
        //public int Id { get; set; }
        public DateTime InterviewDate { get; set; }
        //public C.Company Company { get; set; }
        //public Vacancy Vacancy { get; set; }
        //public List<int> CandidateIds { get; set; }
        public string InterviewLocation { get; set; }
        public string Message { get; set; }
    }
}
