using JobStream.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.DataAccess.Configurations
{
	public class RubricForArticlesConfiguration : IEntityTypeConfiguration<RubricForArticles>
	{
		public void Configure(EntityTypeBuilder<RubricForArticles> builder)
		{
			builder.Property(n => n.Name).IsRequired(true);
		}
	}
}
