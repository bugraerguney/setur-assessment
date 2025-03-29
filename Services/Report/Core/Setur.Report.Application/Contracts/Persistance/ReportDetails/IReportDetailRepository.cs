using Setur.Report.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Report.Application.Contracts.Persistance.ReportDetails
{
    public interface IReportDetailRepository : IGenericRepository<ReportDetail,Guid>
    {
    }
}
