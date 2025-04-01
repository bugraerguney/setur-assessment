using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using Setur.Report.Application.Features.ReportContacts;
using Setur.Report.Application.Features.Services;
using Setur.Report.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Report.Application.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.SuppressModelStateInvalidFilter = true;
            });
            services.AddSingleton(sp =>
                new ConnectionFactory
                {
                    Uri = new Uri(configuration.GetConnectionString("RabbitMQ")),
                    DispatchConsumersAsync = true
                });

            services.AddScoped<IReportContactService, ReportContactService>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddSingleton<RabbitMQPublisher>();

            services.AddSingleton<RabbitMQClientService>();


            return services;
        }
    }
}
