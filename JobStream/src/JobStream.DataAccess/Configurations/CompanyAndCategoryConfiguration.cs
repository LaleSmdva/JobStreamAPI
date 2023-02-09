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
	public class CompanyAndCategoryConfiguration : IEntityTypeConfiguration<CompanyAndCategory>
	{
		public void Configure(EntityTypeBuilder<CompanyAndCategory> builder)
		{
			builder.HasKey(c => new { c.CompanyId, c.CategoryId });
			builder.HasOne(c => c.Company).WithMany(cd => cd.CompanyAndCategories)
				.HasForeignKey(c => c.CompanyId);
			builder.HasOne(c => c.Category).WithMany(cd => cd.CompanyAndCategories)
				.HasForeignKey(c => c.CategoryId);
		}
	}
}
