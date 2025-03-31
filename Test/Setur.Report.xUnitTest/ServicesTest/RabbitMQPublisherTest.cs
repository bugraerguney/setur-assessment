 using Moq;
using RabbitMQ.Client;
using Setur.Report.Application.Features.Services;
 using Setur.Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Setur.Report.xUnitTest.ServicesTest
{
    public class RabbitMQPublisherTest
    {
        [Fact]
        public async Task PublishAsync_Should_Call_Connect_And_BasicPublish()
        {
            // Arrange
            var mockClientService = new Mock<IRabbitMQClientService>();
            var mockChannel = new Mock<IModel>();
            var mockProps = new Mock<IBasicProperties>();

            mockChannel.Setup(c => c.CreateBasicProperties()).Returns(mockProps.Object);
            mockClientService.Setup(c => c.Connect()).Returns(mockChannel.Object);

            var publisher = new RabbitMQPublisher(mockClientService.Object);

            var message = new ReportRequestedMessage
            {
                ReportId = Guid.NewGuid()
            };

            // Act
            await publisher.PublishAsync(message);

            // Assert
            mockChannel.Verify(c =>
     c.BasicPublish(
         RabbitMQClientService.ExchangeName,
         RabbitMQClientService.RoutingReport,
         false,
         mockProps.Object,
         It.IsAny<ReadOnlyMemory<byte>>()
),
     Times.Once);

        }



    }





    }
     
 