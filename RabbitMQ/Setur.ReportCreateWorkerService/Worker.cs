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
namespace Setur.ReportCreateWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly RabbitMQClientService _rabbitMQClientService;
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

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

                     var client = httpClientFactory.CreateClient("contactapi");
                    var response = await client.GetAsync("http://localhost:7176/api/PersonInfos/GetPersonStatistics");

                    response.EnsureSuccessStatusCode();

                    var json = await response.Content.ReadAsStringAsync();
                    var wrapper = JsonSerializer.Deserialize<ServiceResponse<List<PersonStatisticDto>>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    var statistics = wrapper?.Data;

                    if (statistics != null && statistics.Count > 0)
                    {
                        foreach (var stat in statistics)
                        {
                            await detailRepo.AddAsync(new ReportDetail
                            {
                                Id = Guid.NewGuid(),
                                ReportId = message!.ReportId,
                                Location = stat.Location,
                                PersonCount = stat.PersonCount,
                                PhoneNumberCount = stat.PhoneNumberCount
                            });
                        }
                    }

                    var report = await reportRepo.GetByIdAsync(message!.ReportId);
                    if (report is not null)
                    {
                        report.Status = ReportStatus.Completed;
                        report.CompletedAt = DateTime.UtcNow;
                        reportRepo.Update(report);
                    }
                    await unitOfWork.SaveChangesAsync();


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
