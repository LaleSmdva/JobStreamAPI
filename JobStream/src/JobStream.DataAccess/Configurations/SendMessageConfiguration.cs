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
	public class SendMessageConfiguration : IEntityTypeConfiguration<SendMessage>
	{
		public void Configure(EntityTypeBuilder<SendMessage> builder)
		{
			builder.Property(n => n.Fullname).IsRequired(true);
			builder.Property(n => n.Email).IsRequired(true).HasMaxLength(254);
			builder.Property(n => n.Header).IsRequired(true).HasMaxLength(100);
			builder.Property(n => n.Message).IsRequired(true);
		}
	}
}
