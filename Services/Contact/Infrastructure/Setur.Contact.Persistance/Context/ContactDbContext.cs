using Microsoft.EntityFrameworkCore;
using Setur.Contact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.Persistance.Context
{
    public class ContactDbContext(DbContextOptions<ContactDbContext> options) : DbContext(options)
    {
        public DbSet<PersonInfo> PersonInfos { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
