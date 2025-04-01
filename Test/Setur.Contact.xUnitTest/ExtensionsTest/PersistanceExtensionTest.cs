using Castle.Core.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Setur.Contact.Application.Contracts.Persistance;
using Setur.Contact.Application.Contracts.Persistance.ContactInfos;
using Setur.Contact.Application.Contracts.Persistance.PersonInfos;
using Setur.Contact.Domain.Options;
using Setur.Contact.Persistance;
using Setur.Contact.Persistance.ContactInfos;
using Setur.Contact.Persistance.Context;
using Setur.Contact.Persistance.PersonInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.xUnitTest.ExtensionsTest
{
    public class PersistanceExtensionsTest
    {
        [Fact]
        public void AddRepository_Should_Register_All_Repositories()
        {
            // Arrange
            var services = new ServiceCollection();

             services.AddDbContext<ContactDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
            });

             services.AddScoped<IContactInfoRepository, ContactInfoRepository>();
            services.AddScoped<IPersonInfoRepository, PersonInfoRepository>();
            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Act
            var provider = services.BuildServiceProvider();

            // Assert
            Assert.NotNull(provider.GetService<IContactInfoRepository>());
            Assert.NotNull(provider.GetService<IPersonInfoRepository>());
            Assert.NotNull(provider.GetService<IUnitOfWork>());
        }
    }
}
