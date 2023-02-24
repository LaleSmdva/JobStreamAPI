using JobStream.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Core.Entities
{
	public class JobType : IEntity  //full-time,part-time
	{
		public int Id { get; set; }
		public string Name { get; set; }
		//public ICollection<CandidateResume>? CandidateResumes { get; set; }
		public ICollection<Vacancy>? Vacancies { get; set; }
	}
}
