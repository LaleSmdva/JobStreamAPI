using JobStream.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Core.Entities
{
	public class CompanyAndCategory : IEntity
	{
		public int Id { get; set; }
		public int CompanyId { get; set; }
		public Company Company { get; set; }
		public int CategoryId { get; set; }
		public Category Category { get; set; }
	}
}
