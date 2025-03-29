using Setur.Report.Application.Contracts.Persistance.ReportContacts;
using Setur.Report.Application.Features.Services;
using Setur.Report.Domain.Entities;
using Setur.Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Report.Application.Features.ReportContacts
{
    public class ReportContactService : IReportContactService
    {
        private readonly IReportContactRepository _reportRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRabbitMqPublisher _messagePublisher;

        public ReportContactService(
            IReportContactRepository reportRepository,
            IUnitOfWork unitOfWork,
            IRabbitMqPublisher messagePublisher)
        {
            _reportRepository = reportRepository;
            _unitOfWork = unitOfWork;
            _messagePublisher = messagePublisher;
        }

        public async Task<ServiceResult<Guid>> CreateAsync()
        {
            var report = new ReportContact
            {
                Id = Guid.NewGuid(),
                RequestedAt = DateTime.UtcNow,
                Status = ReportStatus.Preparing
            };

            await _reportRepository.AddAsync(report);
            await _unitOfWork.SaveChangesAsync();

            await _messagePublisher.PublishAsync(new ReportRequestedMessage
            {
                ReportId = report.Id
            });

            return ServiceResult<Guid>.SuccessAsCreated(report.Id, $"api/reports/{report.Id}");
        }
    }
}
