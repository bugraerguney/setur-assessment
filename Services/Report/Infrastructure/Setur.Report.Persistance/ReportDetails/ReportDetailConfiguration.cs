using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Setur.Report.Domain.Entities;

namespace Setur.Report.Persistance.ReportDetails
{
    public class ReportDetailConfiguration : IEntityTypeConfiguration<ReportDetail>
    {
        public void Configure(EntityTypeBuilder<ReportDetail> builder)
        {
            builder.HasKey(rd => rd.Id);

            builder.Property(rd => rd.Location)
                   .IsRequired()
                   .HasMaxLength(64);

            builder.Property(rd => rd.PersonCount)
                   .IsRequired();

            builder.Property(rd => rd.PhoneNumberCount)
                   .IsRequired();

            builder.HasOne(rd => rd.Report)
                   .WithMany(r => r.Details)
                   .HasForeignKey(rd => rd.ReportId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
