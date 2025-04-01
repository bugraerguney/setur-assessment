using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Report.Application.Features.ReportDetails.Dtos
{
    public record ReportDetailDto(
        Guid? Id,
        Guid? ReportId,
    string? Location,
    int? PersonCount,
    int? PhoneNumberCount
);
}
