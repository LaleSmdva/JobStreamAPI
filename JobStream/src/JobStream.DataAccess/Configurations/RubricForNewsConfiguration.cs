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
	public class RubricForNewsConfiguration : IEntityTypeConfiguration<RubricForNews>
	{
		public void Configure(EntityTypeBuilder<RubricForNews> builder)
		{
			builder.Property(r => r.Name).IsRequired(true);
		}
	}
}
