using Microsoft.EntityFrameworkCore;
using Setur.Contact.Domain.Entities;
using Setur.Contact.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.xUnitTest.ConfigurationsTest.ContactInfos
{
    public class ContactInfoConfigurationTest
    {
        private readonly ContactDbContext _context;

        public ContactInfoConfigurationTest()
        {
            var options = new DbContextOptionsBuilder<ContactDbContext>()
                .UseInMemoryDatabase("ContactInfoConfigTest")
                .Options;

            _context = new ContactDbContext(options);
        }

        [Fact]
        public void Content_Property_Should_Have_MaxLength_And_IsRequired()
        {
            var entity = _context.Model.FindEntityType(typeof(ContactInfo));
            var prop = entity.FindProperty(nameof(ContactInfo.Content));

            Assert.False(prop.IsNullable);
            Assert.Equal(64, prop.GetMaxLength());
        }

        [Fact]
        public void InfoType_Should_Be_Enum_And_IsRequired()
        {
            var entity = _context.Model.FindEntityType(typeof(ContactInfo));
            var prop = entity.FindProperty(nameof(ContactInfo.InfoType));

            Assert.NotNull(prop);
            Assert.True(prop.ClrType.IsEnum);    
            Assert.False(prop.IsNullable);    
        }
    }
}
