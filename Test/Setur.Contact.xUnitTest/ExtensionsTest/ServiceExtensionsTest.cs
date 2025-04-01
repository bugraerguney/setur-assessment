using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Setur.Contact.Application.Features.ContactInfos;
using Setur.Contact.Application.Features.PersonInfos;
using Setur.Contact.Application.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Setur.Contact.Application.Contracts.Persistance.PersonInfos;
using Moq;
using Setur.Contact.Application.Contracts.Persistance;
using Setur.Contact.Application.Contracts.Persistance.ContactInfos;

namespace Setur.Contact.xUnitTest.ExtensionsTest
{
    public class ServiceExtensionsTest
    {
        [Fact]
        public void AddServices_Should_Register_All_Dependencies()
        {
            // Arrange
            var services = new ServiceCollection();
            var config = new ConfigurationBuilder().Build();

             services.AddSingleton(new Mock<IPersonInfoRepository>().Object);
            services.AddSingleton(new Mock<IContactInfoRepository>().Object);
            services.AddSingleton(new Mock<IUnitOfWork>().Object);

            // Act
            services.AddServices(config);
            var provider = services.BuildServiceProvider();

            // Assert 
            var personService = provider.GetService<IPersonInfoService>();
            var contactService = provider.GetService<IContactInfoService>();
            Assert.NotNull(personService);
            Assert.NotNull(contactService);

             var mvcOptions = services.FirstOrDefault(s => s.ServiceType == typeof(IConfigureOptions<ApiBehaviorOptions>));
            Assert.NotNull(mvcOptions);

             var mapper = provider.GetService<IMapper>();
            Assert.NotNull(mapper);

             var validatorFactory = provider.GetService<IValidatorFactory>();
            Assert.NotNull(validatorFactory);
        }
    }
}
