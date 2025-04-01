using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Setur.Report.Application.Contracts.Persistance.ReportContacts;
using Setur.Report.Application.Contracts.Persistance.ReportDetails;
using Setur.Report.Domain.Entities;
using Setur.ReportCreateWorkerService.Services;
using Setur.Shared.Dtos;
using Setur.Shared.Messages;
using System.Text;
using System.Text.Json;
using System.Net.Http;
using Setur.Shared.ResponseData;
using Setur.Report.Application;
using Setur.ReportCreateWorkerService.Services.Reports;
namespace Setur.ReportCreateWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly RabbitMQClientService _rabbitMQClientService;
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IReportProcessService _reportProcessService;
        private IModel _channel;

        public Worker(
            RabbitMQClientService rabbitMQClientService,
            ILogger<Worker> logger,
            IServiceScopeFactory serviceScopeFactory)
        {
            _rabbitMQClientService = rabbitMQClientService;
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;

            _channel = _rabbitMQClientService.Connect();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);

            _channel.BasicConsume(queue: RabbitMQClientService.QueueName, autoAck: false, consumer: consumer);

            consumer.Received += async (model, ea) =>
            {
                await Task.Delay(5000);

                var body = ea.Body.ToArray();
                var messageJson = Encoding.UTF8.GetString(body);
                var message = JsonSerializer.Deserialize<ReportRequestedMessage>(messageJson);

                _logger.LogInformation($"Mesaj alýndý. ReportId: {message?.ReportId}");

                try
                {
                     using var scope = _serviceScopeFactory.CreateScope();

                    var reportRepo = scope.ServiceProvider.GetRequiredService<IReportContactRepository>();
                    var detailRepo = scope.ServiceProvider.GetRequiredService<IReportDetailRepository>();
                    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                    var httpClientFactory = scope.ServiceProvider.GetRequiredService<IHttpClientFactory>();
                    var reportProcessService = scope.ServiceProvider.GetRequiredService<IReportProcessService>();

                    await reportProcessService.ProcessAsync(message.ReportId);


                    _channel.BasicAck(ea.DeliveryTag, multiple: false);
                    _logger.LogInformation("Rapor iþleme tamamlandý.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Rapor iþleme baþarýsýz oldu.");
                    // Hatalý mesajý geri kuyruklamak için iþlem yapýlabilir
                }
            };

            return Task.CompletedTask;
        }

    }
}
