using JobStream.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Core.Entities
{
	public class Article : IEntity
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public DateTime? PostedOn { get; set; }
		public int RubricForArticlesId { get; set; }
		public RubricForArticles RubricForArticles { get; }
	}
}
