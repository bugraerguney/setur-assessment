using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Setur.Report.Domain.Entities;

namespace Setur.Report.Persistance.Report
{
    public class ReportConfiguration : IEntityTypeConfiguration<ReportContact>
    {
        public void Configure(EntityTypeBuilder<ReportContact> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.RequestedAt)
                   .IsRequired();

            builder.Property(r => r.CompletedAt);

            builder.Property(r => r.Status)
                   .HasConversion<int>()
                   .IsRequired();

            builder.HasMany(r => r.Details)
                   .WithOne(d => d.Report)
                   .HasForeignKey(d => d.ReportId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
