using Setur.Report.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Report.Application.Contracts.Persistance.ReportContacts
{
    public interface IReportContactRepository : IGenericRepository<ReportContact,Guid>
    {
        Task<ReportContact> GetReportWithDetailsAsync(Guid id);
    }
}
