using JobStream.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Core.Entities
{
	public class JobSchedule : IEntity
	{
		public int Id { get; set; }
		public string Schedule { get; set; }
		public ICollection<Vacancy>? Vacancies { get; set; }
	}
}
