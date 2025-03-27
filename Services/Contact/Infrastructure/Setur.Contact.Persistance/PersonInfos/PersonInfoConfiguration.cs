using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Setur.Contact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.Persistance.PersonInfos
{
    public class PersonInfoConfiguration : IEntityTypeConfiguration<PersonInfo>
    {
        public void Configure(EntityTypeBuilder<PersonInfo> builder)
        {
            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(64);

             builder.Property(p => p.Surname)
                   .IsRequired()
                   .HasMaxLength(64);

             builder.Property(p => p.Company)
                   .IsRequired(false)
                   .HasMaxLength(64);
 

             builder.HasMany(p => p.ContactInfos)
                   .WithOne(c => c.PersonInfo)
                   .HasForeignKey(c => c.PersonInfoId)
                   .OnDelete(DeleteBehavior.Cascade);  
        }
    }
}
