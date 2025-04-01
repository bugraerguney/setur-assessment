using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Setur.Report.Application;
using Setur.Report.Application.Contracts.Persistance;
using Setur.Report.Application.Contracts.Persistance.ReportContacts;
using Setur.Report.Application.Contracts.Persistance.ReportDetails;
using Setur.Report.Application.Features.ReportContacts;
using Setur.Report.Domain.Options;
using Setur.Report.Persistance.Context;
using Setur.Report.Persistance.ReportContacts;
using Setur.Report.Persistance.ReportDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Report.Persistance.Extensions
{
    public static class PersistanceExtensions
    {
        public static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ReportDbContext>(opt =>
            {
                var connectionStrings = configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();
                opt.UseNpgsql(connectionStrings!.Npgsql, sqlServerOptionsAction =>
                {
                    sqlServerOptionsAction.MigrationsAssembly(typeof(PersistanceAssembly).Assembly.FullName);
                });
             });
            services.AddScoped<IReportContactRepository, ReportContactRepository>();
            services.AddScoped<IReportDetailRepository, ReportDetailRepository>();
            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
