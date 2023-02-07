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
	public class CategoryFieldConfiguration : IEntityTypeConfiguration<CategoryField>
	{
		public void Configure(EntityTypeBuilder<CategoryField> builder)
		{
			builder.Property(cf => cf.Name).IsRequired(true);
			builder.HasOne(cf=>cf.Category).WithMany(c=>c.CategoryField).HasForeignKey(cf=>cf.CategoryId);
		}
	}
}
