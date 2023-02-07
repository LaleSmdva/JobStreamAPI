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
	public class ArticleConfiguration : IEntityTypeConfiguration<Article>
	{
		public void Configure(EntityTypeBuilder<Article> builder)
		{
			builder.HasOne(a => a.RubricForArticles).WithMany(r => r.Articles)
				.HasForeignKey(a => a.RubricForArticlesId);
			builder.Property(b => b.Title).IsRequired(true).HasMaxLength(150);
			builder.Property(b => b.Description).IsRequired(true);
			builder.Property(b => b.PostedOn).IsRequired(true);
		}
	}
}
