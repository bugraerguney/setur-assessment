using Setur.Report.Application.Features.ReportDetails.Dtos;
using Setur.Report.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Report.Application.Features.ReportContacts.Dtos
{
    public record ResultReportWithDetailsDto
    (
        Guid Id,
  DateTime RequestedAt,
    DateTime? CompletedAt,
    ReportStatus Status,
    List<ReportDetailDto> Details
    );
}
