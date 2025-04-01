using Microsoft.EntityFrameworkCore;
using Setur.Contact.Domain.Entities;
using Setur.Contact.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.xUnitTest.ConfigurationsTest.PersonInfos
{
    public class PersonInfoConfigurationTest
    {
        private readonly ContactDbContext _context;

        public PersonInfoConfigurationTest()
        {
            var options = new DbContextOptionsBuilder<ContactDbContext>()
                .UseInMemoryDatabase("PersonInfoConfigTest")
                .Options;

            _context = new ContactDbContext(options);
        }

        [Fact]
        public void Name_Should_Be_Required_And_MaxLength_64()
        {
            var entity = _context.Model.FindEntityType(typeof(PersonInfo));
            var prop = entity.FindProperty(nameof(PersonInfo.Name));

            Assert.NotNull(prop);
            Assert.False(prop.IsNullable);
            Assert.Equal(64, prop.GetMaxLength());
        }

        [Fact]
        public void Surname_Should_Be_Required_And_MaxLength_64()
        {
            var entity = _context.Model.FindEntityType(typeof(PersonInfo));
            var prop = entity.FindProperty(nameof(PersonInfo.Surname));

            Assert.NotNull(prop);
            Assert.False(prop.IsNullable);
            Assert.Equal(64, prop.GetMaxLength());
        }

        [Fact]
        public void Company_Should_Be_Optional_And_MaxLength_64()
        {
            var entity = _context.Model.FindEntityType(typeof(PersonInfo));
            var prop = entity.FindProperty(nameof(PersonInfo.Company));

            Assert.NotNull(prop);
            Assert.True(prop.IsNullable);
            Assert.Equal(64, prop.GetMaxLength());
        }

        [Fact]
        public void ContactInfos_Should_Have_Cascade_Delete()
        {
            var entity = _context.Model.FindEntityType(typeof(ContactInfo));
            var foreignKey = entity.GetForeignKeys()
                .FirstOrDefault(fk => fk.PrincipalEntityType.ClrType == typeof(PersonInfo));

            Assert.NotNull(foreignKey);
            Assert.Equal(DeleteBehavior.Cascade, foreignKey.DeleteBehavior);
        }

        [Fact]
        public void InfoType_Should_Be_Enum_And_Required()
        {
            var entity = _context.Model.FindEntityType(typeof(ContactInfo));
            var prop = entity.FindProperty(nameof(ContactInfo.InfoType));

            Assert.NotNull(prop);
            Assert.True(prop.ClrType.IsEnum);
            Assert.False(prop.IsNullable);
        }
    }
}
