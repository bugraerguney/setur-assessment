using Castle.Core.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RabbitMQ.Client;
using Setur.Report.Application.Features.ReportContacts;
using Setur.Report.Application.Features.Services;
using Setur.Report.Application.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Setur.Report.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Setur.Report.Application.Contracts.Persistance.ReportContacts;
using Setur.Report.Persistance.ReportContacts;
using Setur.Report.Persistance.ReportDetails;
using Setur.Report.Application.Contracts.Persistance.ReportDetails;
using Setur.Report.Application.Contracts.Persistance;
using Setur.Report.Persistance;
using Setur.Report.Application;
using Microsoft.Extensions.Logging;

namespace Setur.Report.xUnitTest.ExtensionsTest
{
    public class ServiceExtensionsTest
    {
        [Fact]
        public void AddServices_Should_Register_All_Services_Correctly()
        {
            // Arrange
            var services = new ServiceCollection();

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
            { "ConnectionStrings:RabbitMQ", "amqp://localhost" }
                })
                .Build();

            services.AddLogging();

            services.AddSingleton(new Mock<IReportContactRepository>().Object);
            services.AddSingleton(new Mock<IReportDetailRepository>().Object);
            services.AddSingleton(new Mock<IUnitOfWork>().Object);

            var factory = new ConnectionFactory();
            var logger = new Mock<ILogger<RabbitMQClientService>>().Object;

            services.AddSingleton<IRabbitMQClientService>(
                new RabbitMQClientService(factory, logger));

            services.AddServices(configuration);

            var provider = services.BuildServiceProvider();

            // Assert
            Assert.NotNull(provider.GetService<IReportContactService>());
            Assert.NotNull(provider.GetService<IRabbitMqPublisher>());
            Assert.NotNull(provider.GetService<IRabbitMQClientService>());
            Assert.NotNull(provider.GetService<IMapper>());
        }
    }
}
