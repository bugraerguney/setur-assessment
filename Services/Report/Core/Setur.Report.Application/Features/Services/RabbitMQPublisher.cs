using RabbitMQ.Client;
using Setur.Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Setur.Report.Application.Features.Services
{
    public class RabbitMQPublisher
    {
        private readonly RabbitMQClientService _rabbitmqClientService;

        public RabbitMQPublisher(RabbitMQClientService rabbitmqClientService)
        {
            _rabbitmqClientService = rabbitmqClientService;
        }

        public async Task PublishAsync(ReportRequestedMessage  reportRequestedMessage)
        {
            var channel = _rabbitmqClientService.Connect();

            var bodyString = JsonSerializer.Serialize(reportRequestedMessage);
            var bodyByte = Encoding.UTF8.GetBytes(bodyString);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(
       exchange: RabbitMQClientService.ExchangeName,
       routingKey: RabbitMQClientService.RoutingReport,
        properties,
        bodyByte);


        }
    }
}
