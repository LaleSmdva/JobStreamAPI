using JobStream.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Core.Entities
{
	public class CandidateEducation 
	{
		public int Id { get; set; }
        //public string? EducationInfo { get; set; }
        public int? CandidateResumeId { get; set; }
        public CandidateResume? CandidateResume { get; set; }
        //new
        public string? Major { get; set; }
        public string? Degree { get; set; }
        public string? Institution { get; set; }

    }
}
