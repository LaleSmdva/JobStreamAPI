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
	public class JobScheduleConfiguration : IEntityTypeConfiguration<JobSchedule>
	{
		public void Configure(EntityTypeBuilder<JobSchedule> builder)
		{
			builder.Property(j => j.Schedule).IsRequired(true);
		}
	}
}
