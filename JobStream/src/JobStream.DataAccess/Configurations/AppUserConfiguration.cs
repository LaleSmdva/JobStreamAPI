using JobStream.Core.Entities;
using JobStream.Core.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.DataAccess.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasOne(b => b.CandidateResume).WithOne(c => c.AppUser)
                .HasForeignKey<CandidateResume>(s => s.AppUserId);

            builder.HasOne(b => b.Company).WithOne(c => c.AppUser)
                .HasForeignKey<Company>(s => s.UserId);

        }
    }
}
