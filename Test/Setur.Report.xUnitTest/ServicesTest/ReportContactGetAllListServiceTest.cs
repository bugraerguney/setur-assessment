using AutoMapper;
using FluentAssertions;
using Moq;
using Setur.Report.Application;
using Setur.Report.Application.Contracts.Persistance.ReportContacts;
using Setur.Report.Application.Features.ReportContacts;
using Setur.Report.Application.Features.ReportContacts.Dtos;
using Setur.Report.Application.Features.Services;
using Setur.Report.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Report.xUnitTest.ServicesTest
{
    public class ReportContactGetAllListServiceTest
    {
        private readonly Mock<IReportContactRepository> _mockRepo;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IRabbitMqPublisher> _mockPublisher;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ReportContactService _service;

        public ReportContactGetAllListServiceTest()
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
        public async Task GetAllListAsync_Should_Return_All_Reports()
        {
            // Arrange
            var reports = new List<ReportContact>
            {
                new() { Id = Guid.NewGuid(), RequestedAt = DateTime.UtcNow, Status = ReportStatus.Completed, CompletedAt = DateTime.UtcNow.AddMinutes(2) },
                new() { Id = Guid.NewGuid(), RequestedAt = DateTime.UtcNow, Status = ReportStatus.Preparing, CompletedAt = null }
            };

            var mapped = new List<ResultReportContactDto>
            {
                new() { Id = reports[0].Id, RequestedAt = reports[0].RequestedAt, CompletedAt = reports[0].CompletedAt, Status = reports[0].Status },
                new() { Id = reports[1].Id, RequestedAt = reports[1].RequestedAt, CompletedAt = reports[1].CompletedAt, Status = reports[1].Status }
            };

            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(reports);
            _mockMapper.Setup(m => m.Map<List<ResultReportContactDto>>(reports)).Returns(mapped);

            // Act
            var result = await _service.GetAllListAsync();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.Should().BeEquivalentTo(mapped);
            result.Status.Should().Be(HttpStatusCode.OK);
        }
    }
}
