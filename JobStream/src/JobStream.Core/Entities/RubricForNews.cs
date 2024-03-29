﻿using JobStream.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Core.Entities
{
	public class RubricForNews : IEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public ICollection<News>? News { get; set; }
	}
}
