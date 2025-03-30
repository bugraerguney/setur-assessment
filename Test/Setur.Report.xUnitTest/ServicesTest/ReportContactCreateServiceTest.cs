using AutoMapper;
using FluentAssertions;
using Moq;
using Setur.Report.Application;
using Setur.Report.Application.Contracts.Persistance.ReportContacts;
using Setur.Report.Application.Features.ReportContacts;
using Setur.Report.Application.Features.Services;
using Setur.Report.Domain.Entities;
using Setur.Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Report.xUnitTest.ServicesTest
{
    public class ReportContactCreateServiceTest
    {
        private readonly Mock<IReportContactRepository> _mockRepo;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IRabbitMqPublisher> _mockPublisher;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ReportContactService _service;

        public ReportContactCreateServiceTest()
        {
            _mockRepo = new Mock<IReportContactRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockPublisher = new Mock<IRabbitMqPublisher>();
            _mockMapper = new Mock<IMapper>();

            _service = new ReportContactService(
                _mockRepo.Object,
                _mockUnitOfWork.Object,
                _mockPublisher.Object,
                _mockMapper.Object
            );
        }

        [Fact]
        public async Task CreateAsync_Should_Create_Report_And_Publish_Message()
        {
            // Arrange
            var reportId = Guid.NewGuid();

            _mockRepo.Setup(r => r.AddAsync(It.IsAny<ReportContact>()))
                .Returns(ValueTask.CompletedTask);

            _mockUnitOfWork.Setup(u => u.SaveChangesAsync())
                .ReturnsAsync(1);

            _mockPublisher.Setup(p => p.PublishAsync(It.IsAny<ReportRequestedMessage>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _service.CreateAsync();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Status.Should().Be(HttpStatusCode.Created);
            result.Data.Should().NotBeEmpty();

            _mockRepo.Verify(r => r.AddAsync(It.IsAny<ReportContact>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
            _mockPublisher.Verify(p => p.PublishAsync(It.IsAny<ReportRequestedMessage>()), Times.Once);
        }
    }
}
