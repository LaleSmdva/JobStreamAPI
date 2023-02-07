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
	public class VacancyConfiguration : IEntityTypeConfiguration<Vacancy>
	{
		public void Configure(EntityTypeBuilder<Vacancy> builder)
		{
			builder.HasOne(v => v.Company).WithMany(c => c.Vacancies).HasForeignKey(v => v.CompanyId);
			builder.HasOne(v=>v.Category).WithMany(c=>c.Vacancies).HasForeignKey(v=>v.CategoryId);
			builder.HasOne(v => v.JobType).WithMany(j => j.Vacancies).HasForeignKey(v=>v.JobTypeId);
			builder.HasOne(v => v.JobSchedule).WithMany(j => j.Vacancies).HasForeignKey(v => v.JobScheduleId);

			builder.Property(v => v.Name).IsRequired(true).HasMaxLength(100);
			builder.Property(v => v.Location).IsRequired(true);
			builder.Property(v => v.Salary).IsRequired(true);
			builder.Property(v => v.Requirements).IsRequired(true);
			builder.Property(v => v.Description).IsRequired(true);
			builder.Property(v => v.ExperienceLevel).IsRequired(true);

		}
	}
}
