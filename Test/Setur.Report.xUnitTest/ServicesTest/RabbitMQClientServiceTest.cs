using Microsoft.Extensions.Logging;
using Moq;
using RabbitMQ.Client;
using Setur.Report.Application.Features.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Report.xUnitTest.ServicesTest
{
    public class RabbitMQClientServiceTest
    {
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        public static string ExchangeName = "ReportDirectExchange";
        public static string RoutingReport = "report-root-file";
        public static string QueueName = "queue-report";
        private readonly ILogger<RabbitMQClientService> _logger;

         public RabbitMQClientServiceTest(IConnection connection, IModel channel, ILogger<RabbitMQClientService> logger)
        {
            _connection = connection;
            _channel = channel;
            _logger = logger;
        }

        public RabbitMQClientServiceTest(ConnectionFactory connectionFactory, ILogger<RabbitMQClientService> logger)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IModel Connect()
        {
            if (_channel is not null && _channel.IsOpen)
            {
                return _channel;
            }

            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(ExchangeName, type: "direct", durable: true, autoDelete: false);
            _channel.QueueDeclare(QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(exchange: ExchangeName, queue: QueueName, routingKey: RoutingReport);

            _logger.LogInformation("RabbitMQ ile bağlantı kuruldu");

            return _channel;
        }

        public void Dispose()
        {
            if (_channel is { IsOpen: true })
            {
                _channel.Close();
                _channel.Dispose();
            }

            if (_connection is { IsOpen: true })
            {
                _connection.Close();
                _connection.Dispose();
            }

            _logger.LogInformation("RabbitMQ bağlantısı kapatıldı");
        }
    }
}
