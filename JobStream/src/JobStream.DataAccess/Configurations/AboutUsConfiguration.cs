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
	public class AboutUsConfiguration : IEntityTypeConfiguration<AboutUs>
	{
		public void Configure(EntityTypeBuilder<AboutUs> builder)
		{
			builder.Property(b => b.Location).IsRequired(true).HasMaxLength(150);
			builder.Property(b => b.Email).IsRequired(true);
			builder.Property(b => b.Telephone).IsRequired(true);
			builder.Property(b => b.FacebookLink).HasColumnName("Facebook");
			builder.Property(b => b.InstagramLink).HasColumnName("Instagram");
			builder.Property(b => b.LinkedinLink).HasColumnName("Linkedin");
		}
	}
}
