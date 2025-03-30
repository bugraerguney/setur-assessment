using AutoMapper;
using FluentAssertions;
using Moq;
using Setur.Report.Application;
using Setur.Report.Application.Contracts.Persistance.ReportContacts;
using Setur.Report.Application.Features.ReportContacts;
using Setur.Report.Application.Features.ReportContacts.Dtos;
using Setur.Report.Application.Features.ReportDetails.Dtos;
using Setur.Report.Application.Features.Services;
using Setur.Report.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Report.xUnitTest.ServicesTest
{
    public class ReportContactWithDetailsServiceTest
    {
        private readonly Mock<IReportContactRepository> _mockRepo;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IRabbitMqPublisher> _mockPublisher;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ReportContactService _service;

        public ReportContactWithDetailsServiceTest()
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
        public async Task GetByReportIdWithDetailsAsync_Should_Return_Report_With_Details()
        {
            // Arrange
            var reportId = Guid.NewGuid();
            var reportEntity = new ReportContact
            {
                Id = reportId,
                RequestedAt = DateTime.UtcNow,
                CompletedAt = DateTime.UtcNow.AddMinutes(5),
                Status = ReportStatus.Completed,
                Details = new List<ReportDetail>
                {
                    new() { Id = Guid.NewGuid(), ReportId = reportId, Location = "İstanbul", PersonCount = 2, PhoneNumberCount = 3 }
                }
            };

            var mappedDto = new ResultReportWithDetailsDto(
                reportId,
                reportEntity.RequestedAt,
                reportEntity.CompletedAt,
                reportEntity.Status,
                new List<ReportDetailDto>
                {
                    new(reportEntity.Details[0].Id, reportId, "İstanbul", 2, 3)
                }
            );

            _mockRepo.Setup(r => r.GetReportWithDetailsAsync(reportId)).ReturnsAsync(reportEntity);
            _mockMapper.Setup(m => m.Map<ResultReportWithDetailsDto>(reportEntity)).Returns(mappedDto);

            // Act
            var result = await _service.GetByReportIdWithDetailsAsync(reportId);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data!.Id.Should().Be(reportId);
            result.Data.Details.Should().HaveCount(1);
        }
    }
}
