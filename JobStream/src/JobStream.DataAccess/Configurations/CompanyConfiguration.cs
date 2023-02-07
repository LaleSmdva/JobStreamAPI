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
	public class CompanyConfiguration : IEntityTypeConfiguration<Company>
	{
		public void Configure(EntityTypeBuilder<Company> builder)
		{
			builder.Property(c=>c.Name).IsRequired(true).HasMaxLength(100);
			builder.Property(c=>c.Location).IsRequired(true).HasMaxLength(200);
			builder.Property(c=>c.AboutCompany).IsRequired(true);
			builder.Property(c=>c.Email).IsRequired(true);


		}
	}
}
