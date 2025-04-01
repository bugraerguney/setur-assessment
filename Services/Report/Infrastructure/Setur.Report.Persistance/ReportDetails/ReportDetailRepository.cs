using Setur.Report.Application.Contracts.Persistance;
using Setur.Report.Application.Contracts.Persistance.ReportContacts;
using Setur.Report.Application.Contracts.Persistance.ReportDetails;
using Setur.Report.Domain.Entities;
using Setur.Report.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Report.Persistance.ReportDetails
{
    public class ReportDetailRepository(ReportDbContext context) : GenericRepository<ReportDetail,Guid>(context), IReportDetailRepository
    {
     
    }
}
