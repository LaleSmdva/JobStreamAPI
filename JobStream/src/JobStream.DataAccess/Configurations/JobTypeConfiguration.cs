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
	public class JobTypeConfiguration : IEntityTypeConfiguration<JobType> //full-time,part-time
	{
		public void Configure(EntityTypeBuilder<JobType> builder)
		{
			builder.Property(j => j.Name).IsRequired(true).HasMaxLength(50);
		}
	}
}
