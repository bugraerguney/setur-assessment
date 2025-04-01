using Setur.Report.Application;
using Setur.Report.Application.Contracts.Persistance.ReportContacts;
using Setur.Report.Application.Contracts.Persistance.ReportDetails;
using Setur.Report.Domain.Entities;
using Setur.Shared.Dtos;
using Setur.Shared.ResponseData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Setur.ReportCreateWorkerService.Services.Reports
{
    public class ReportProcessService : IReportProcessService
    {
        private readonly IReportContactRepository _reportRepo;
        private readonly IReportDetailRepository _detailRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ReportProcessService> _logger;

        public ReportProcessService(
            IReportContactRepository reportRepo,
            IReportDetailRepository detailRepo,
            IUnitOfWork unitOfWork,
            IHttpClientFactory httpClientFactory,
            ILogger<ReportProcessService> logger)
        {
            _reportRepo = reportRepo;
            _detailRepo = detailRepo;
            _unitOfWork = unitOfWork;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task ProcessAsync(Guid reportId)
        {
            var client = _httpClientFactory.CreateClient("contactapi");
            var response = await client.GetAsync("http://localhost:7176/api/PersonInfos/GetPersonStatistics");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var wrapper = JsonSerializer.Deserialize<ServiceResponse<List<PersonStatisticDto>>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var statistics = wrapper?.Data ?? new();

            foreach (var stat in statistics)
            {
                await _detailRepo.AddAsync(new ReportDetail
                {
                    Id = Guid.NewGuid(),
                    ReportId = reportId,
                    Location = stat.Location,
                    PersonCount = stat.PersonCount,
                    PhoneNumberCount = stat.PhoneNumberCount
                });
            }

            var report = await _reportRepo.GetByIdAsync(reportId);
            if (report is not null)
            {
                report.Status = ReportStatus.Completed;
                report.CompletedAt = DateTime.UtcNow;
                _reportRepo.Update(report);
            }

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
