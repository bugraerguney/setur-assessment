using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Report.Application.Features.ReportContacts
{
    public interface IReportContactService
    {
        Task<ServiceResult<Guid>> CreateAsync();

    }
}
