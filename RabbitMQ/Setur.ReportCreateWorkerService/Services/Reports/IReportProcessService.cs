using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.ReportCreateWorkerService.Services.Reports
{
    public interface IReportProcessService
    {
        Task ProcessAsync(Guid reportId);

    }
}
