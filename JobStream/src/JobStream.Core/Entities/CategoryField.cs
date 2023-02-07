using JobStream.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Core.Entities
{
	public class CategoryField : IEntity
	{
		public int Id { get; set; }

		public string? Name { get; set; }
		public int CategoryId { get; set; }
		public Category Category { get; set; }
	}
}
