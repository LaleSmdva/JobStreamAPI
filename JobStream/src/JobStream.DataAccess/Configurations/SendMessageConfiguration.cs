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
			builder.Property(s => s.Fullname).IsRequired(true);
			builder.Property(s => s.Email).IsRequired(true).HasMaxLength(254);
			builder.Property(s => s.Header).IsRequired(true).HasMaxLength(100);
			builder.Property(s => s.Message).IsRequired(true);
		}
	}
}
