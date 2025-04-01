using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using Setur.Report.Persistance.Context;
using Setur.Report.Persistance.Extensions;
using Setur.ReportCreateWorkerService;
using Setur.ReportCreateWorkerService.Services;
using Setur.ReportCreateWorkerService.Services.Reports;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSingleton(sp => new ConnectionFactory() { Uri = new Uri(builder.Configuration.GetConnectionString("RabbitMQ")), DispatchConsumersAsync = true });
builder.Services.AddDbContext<ReportDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("Npgsql"));
});
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddSingleton<RabbitMQClientService>();
builder.Services.AddRepository(builder.Configuration);
 
builder.Services.AddHostedService<Worker>();
builder.Services.AddScoped<IReportProcessService, ReportProcessService>();

builder.Services.AddHttpClient();
var host = builder.Build();
host.Run();
