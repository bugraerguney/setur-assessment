using AutoMapper;
using Setur.Report.Application.Contracts.Persistance.ReportContacts;
using Setur.Report.Application.Features.ReportContacts.Dtos;
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
        private readonly RabbitMQPublisher _messagePublisher;
        private readonly IMapper _mapper;

        public ReportContactService(
            IReportContactRepository reportRepository,
            IUnitOfWork unitOfWork,
            RabbitMQPublisher messagePublisher,
            IMapper mapper)
        {
            _reportRepository = reportRepository;
            _unitOfWork = unitOfWork;
            _messagePublisher = messagePublisher;
            _mapper = mapper;
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

            return ServiceResult<Guid>.SuccessAsCreated(report.Id, $"api/reportcontacts/{report.Id}");
        }

        public async Task<ServiceResult<List<ResultReportContactDto>>> GetAllListAsync()
        {
            var contactInfos = await _reportRepository.GetAllAsync();

            var contactInfosDto = _mapper.Map<List<ResultReportContactDto>>(contactInfos);

            return ServiceResult<List<ResultReportContactDto>>.Success(contactInfosDto);
        }

        public async Task<ServiceResult<ResultReportWithDetailsDto>> GetByReportIdWithDetailsAsync(Guid id)
        {
            var report = await _reportRepository.GetReportWithDetailsAsync(id);


            var reportDto = _mapper.Map<ResultReportWithDetailsDto>(report);

            return ServiceResult<ResultReportWithDetailsDto>.Success(reportDto);
        }
    }
}
