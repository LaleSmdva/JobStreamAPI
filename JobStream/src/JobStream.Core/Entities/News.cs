using JobStream.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Core.Entities
{
	public class News : IEntity
	{

        public int Id { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		//Updated 26
		public DateTime? PostedOn { get; set; }
		public int RubricForNewsId { get; set; }
		public RubricForNews RubricForNews { get; }
	}
}
