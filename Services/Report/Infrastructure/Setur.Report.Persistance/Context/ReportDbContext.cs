using Microsoft.EntityFrameworkCore;
using Setur.Report.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Report.Persistance.Context
{
    public class ReportDbContext(DbContextOptions<ReportDbContext> options) : DbContext(options)
    {
        public DbSet<ReportContact> ReportContacts { get; set; }
        public DbSet<ReportDetail> ReportDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
