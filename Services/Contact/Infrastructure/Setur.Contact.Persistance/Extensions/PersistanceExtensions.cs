using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Setur.Contact.Application.Contracts.Persistance;
using Setur.Contact.Application.Contracts.Persistance.ContactInfos;
using Setur.Contact.Application.Contracts.Persistance.PersonInfos;
using Setur.Contact.Domain.Options;
using Setur.Contact.Persistance.ContactInfos;
using Setur.Contact.Persistance.Context;
using Setur.Contact.Persistance.Interceptors;
using Setur.Contact.Persistance.PersonInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.Persistance.Extensions
{
    public static class PersistanceExtensions
    {
        public static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ContactDbContext>(opt =>
            {
                var connectionStrings = configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();
                opt.UseNpgsql(connectionStrings!.Npgsql, sqlServerOptionsAction =>
                {
                    sqlServerOptionsAction.MigrationsAssembly(typeof(PersistanceAssembly).Assembly.FullName);
                });
                opt.AddInterceptors(new AuditDbContextInterceptor());
            });
            services.AddScoped<IContactInfoRepository, ContactInfoRepository>();
            services.AddScoped<IPersonInfoRepository, PersonInfoRepository>();
            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
