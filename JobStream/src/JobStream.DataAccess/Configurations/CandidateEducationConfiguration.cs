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
	public class CandidateEducationConfiguration : IEntityTypeConfiguration<CandidateEducation>
	{
		public void Configure(EntityTypeBuilder<CandidateEducation> builder)
		{
			//builder.HasOne(b => b.CandidateResume).WithOne(c => c.CandidateEducation)
			//.HasForeignKey<CandidateResume>(s => s.Id);

			builder.Property(c => c.EducationInfo).IsRequired(true).HasMaxLength(300);
		}
	}
}
