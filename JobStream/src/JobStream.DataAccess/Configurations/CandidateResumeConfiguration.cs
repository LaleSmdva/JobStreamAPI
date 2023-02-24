using JobStream.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.DataAccess.Configurations
{
	public class CandidateResumeConfiguration : IEntityTypeConfiguration<CandidateResume>
	{
		public void Configure(EntityTypeBuilder<CandidateResume> builder)
		{
			builder.HasOne(b => b.CandidateEducation).WithOne(c => c.CandidateResume)
				.HasForeignKey<CandidateEducation>(s => s.CandidateResumeId);

            //builder.HasOne(b => b.JobType).WithMany(j => j.CandidateResumes).HasForeignKey(c => c.JobTypeId);

            builder.Property(b => b.Fullname).IsRequired(true);
			builder.HasIndex(b => b.Telephone).IsUnique();
			builder.Property(b => b.Email).IsRequired(true);
			builder.Property(b => b.AboutMe).IsRequired(true);
			builder.Property(b => b.IsDeleted).HasDefaultValue(false);
			builder.Property(b => b.WorkExperience).IsRequired(true);
		}
	}
}
