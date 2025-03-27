using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Setur.Contact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.Persistance.ContactInfos
{
    public class ContactInfoConfiguration : IEntityTypeConfiguration<ContactInfo>
    {
        public void Configure(EntityTypeBuilder<ContactInfo> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Content)
                   .IsRequired()
                   .HasMaxLength(64);
            builder.Property(x => x.InfoType)
                  .IsRequired()
                  .HasConversion<int>();
          
        }
    }
}
