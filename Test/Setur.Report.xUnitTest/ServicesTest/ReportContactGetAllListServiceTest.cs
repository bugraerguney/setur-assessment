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
        public async Task GetAllListAsync_Should_Return_Mapped_List()
        {
            // Arrange
            var reports = new List<ReportContact>
            {
                new() { Id = Guid.NewGuid(), RequestedAt = DateTime.UtcNow, Status = ReportStatus.Completed },
                new() { Id = Guid.NewGuid(), RequestedAt = DateTime.UtcNow.AddMinutes(-10), Status = ReportStatus.Preparing }
            };

            var mappedReports = new List<ResultReportContactDto>
            {
                new(reports[0].Id, reports[0].RequestedAt, null, reports[0].Status),
                new(reports[1].Id, reports[1].RequestedAt, null, reports[1].Status)
            };

            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(reports);
            _mockMapper.Setup(m => m.Map<List<ResultReportContactDto>>(reports)).Returns(mappedReports);

            // Act
            var result = await _service.GetAllListAsync();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().BeEquivalentTo(mappedReports);
            result.Status.Should().Be(HttpStatusCode.OK);
        }
    }
}
