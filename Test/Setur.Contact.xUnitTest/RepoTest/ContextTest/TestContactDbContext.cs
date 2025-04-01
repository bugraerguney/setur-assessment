using Microsoft.EntityFrameworkCore;
using Setur.Contact.Domain.Entities.Common;
using Setur.Contact.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.xUnitTest.RepoTest.ContextTest
{
    public class SampleEntity : BaseEntity<Guid>
    {
        public string Name { get; set; }
    }

    public class TestContactDbContext : ContactDbContext
    {
        public TestContactDbContext(DbContextOptions<ContactDbContext> options)
            : base(options)
        {
        }

        public DbSet<SampleEntity> SampleEntities { get; set; }
    }
}
