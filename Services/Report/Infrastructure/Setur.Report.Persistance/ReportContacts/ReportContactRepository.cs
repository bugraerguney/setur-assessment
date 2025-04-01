using Microsoft.EntityFrameworkCore;
using Setur.Report.Application.Contracts.Persistance.ReportContacts;
using Setur.Report.Domain.Entities;
using Setur.Report.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Report.Persistance.ReportContacts
{
    public class ReportContactRepository(ReportDbContext context) : GenericRepository<ReportContact,Guid>(context), IReportContactRepository
    {
        public async Task<ReportContact> GetReportWithDetailsAsync(Guid id)
        {
            return await context.ReportContacts
                    .Include(r => r.Details)
                    .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
