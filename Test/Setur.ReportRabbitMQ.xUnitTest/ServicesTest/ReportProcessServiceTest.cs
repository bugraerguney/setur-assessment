using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Setur.Report.Application;
using Setur.Report.Application.Contracts.Persistance.ReportContacts;
using Setur.Report.Application.Contracts.Persistance.ReportDetails;
using Setur.Report.Domain.Entities;
using Setur.ReportCreateWorkerService.Services.Reports;
using Setur.Shared.Dtos;
using Setur.Shared.ResponseData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Setur.ReportRabbitMQ.xUnitTest.ServicesTest
{
    public class ReportProcessServiceTest
    {
        private readonly Mock<IReportContactRepository> _reportRepoMock;
        private readonly Mock<IReportDetailRepository> _detailRepoMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
        private readonly Mock<ILogger<ReportProcessService>> _loggerMock;
        private readonly ReportProcessService _service;

        public ReportProcessServiceTest()
        {
            _reportRepoMock = new Mock<IReportContactRepository>();
            _detailRepoMock = new Mock<IReportDetailRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
            _loggerMock = new Mock<ILogger<ReportProcessService>>();

            // Fake HTTP response
            var statistics = new List<PersonStatisticDto>
            {
                new() { Location = "İstanbul", PersonCount = 3, PhoneNumberCount = 5 },
                new() { Location = "Ankara", PersonCount = 2, PhoneNumberCount = 3 }
            };

            var responseContent = JsonSerializer.Serialize(new ServiceResponse<List<PersonStatisticDto>>
            {
                Data = statistics
            });

            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent, Encoding.UTF8, "application/json")
                });

            var httpClient = new HttpClient(httpMessageHandlerMock.Object);
            _httpClientFactoryMock.Setup(f => f.CreateClient("contactapi")).Returns(httpClient);

            _service = new ReportProcessService(
                _reportRepoMock.Object,
                _detailRepoMock.Object,
                _unitOfWorkMock.Object,
                _httpClientFactoryMock.Object,
                _loggerMock.Object
            );
        }

        [Fact]
        public async Task ProcessAsync_Should_Save_Details_And_Update_Report()
        {
            // Arrange
            var reportId = Guid.NewGuid();
            var report = new ReportContact { Id = reportId, Status = ReportStatus.Preparing };
            _reportRepoMock.Setup(r => r.GetByIdAsync(reportId)).ReturnsAsync(report);

            // Act
            await _service.ProcessAsync(reportId);

            // Assert
            _detailRepoMock.Verify(r => r.AddAsync(It.IsAny<ReportDetail>()), Times.Exactly(2));
            _reportRepoMock.Verify(r => r.Update(It.Is<ReportContact>(r => r.Status == ReportStatus.Completed)), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }
    }
}
