using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Report.Domain.Entities
{
    public class ReportContact
    {
        public Guid Id { get; set; }
        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; }
        public ReportStatus Status { get; set; } = ReportStatus.Preparing;

        public List<ReportDetail> Details { get; set; } = new List<ReportDetail>();
    }
}
