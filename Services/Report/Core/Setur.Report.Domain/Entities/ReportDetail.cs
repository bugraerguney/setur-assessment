using Setur.Report.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Report.Domain.Entities
{
    public class ReportDetail: BaseEntity<Guid>
    {
         public Guid ReportId { get; set; }

        public string Location { get; set; } = null!;
        public int PersonCount { get; set; }
        public int PhoneNumberCount { get; set; }

        public ReportContact Report { get; set; } = null!;
    }
}
