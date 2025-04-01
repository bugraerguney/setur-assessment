using Setur.Report.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Report.Application.Features.ReportContacts.Dtos
{
    public class ResultReportContactDto
    {
        public Guid Id { get; set; }
        public DateTime RequestedAt { get; set; } 
        public DateTime? CompletedAt { get; set; }
        public ReportStatus Status { get; set; }
    }
}
