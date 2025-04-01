using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Setur.Report.Application;
using Setur.Report.Application.Contracts.Persistance;
using Setur.Report.Application.Contracts.Persistance.ReportContacts;
using Setur.Report.Application.Contracts.Persistance.ReportDetails;
using Setur.Report.Domain.Entities;
using Setur.Report.Domain.Options;
using Setur.Report.Persistance;
using Setur.Report.Persistance.Context;
using Setur.Report.Persistance.Extensions;
using Setur.Report.Persistance.ReportContacts;
using Setur.Report.Persistance.ReportDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Report.xUnitTest.ExtensionsTest
{

    public class PersistanceExtensionsTest
    {
        [Fact]
        public void AddRepository_Should_Register_All_Services_Correctly()
        {
            // Arrange
            var services = new ServiceCollection();

            services.AddDbContext<ReportDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
            });

             services.AddScoped<IReportContactRepository, ReportContactRepository>();
            services.AddScoped<IReportDetailRepository, ReportDetailRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IGenericRepository<ReportContact, Guid>, GenericRepository<ReportContact, Guid>>();

            // Act
            var provider = services.BuildServiceProvider();

            // Assert
            Assert.NotNull(provider.GetService<IReportContactRepository>());
            Assert.NotNull(provider.GetService<IReportDetailRepository>());
            Assert.NotNull(provider.GetService<IUnitOfWork>());
            Assert.NotNull(provider.GetService<IGenericRepository<ReportContact, Guid>>());
        }
    }
}
     
     
 
