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
	public class NewsConfiguration : IEntityTypeConfiguration<News>
	{
		public void Configure(EntityTypeBuilder<News> builder)
		{
			builder.Property(n => n.Title).IsRequired(true);
			builder.Property(n => n.Content).IsRequired(true);
			builder.HasOne(n => n.RubricForNews).WithMany(r => r.News).HasForeignKey(n => n.RubricForNewsId);
		}
	}
}
