using Setur.Report.Application.Features.ReportContacts.Dtos;
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
        Task<ServiceResult<List<ResultReportContactDto>>> GetAllListAsync();
        Task<ServiceResult<ResultReportWithDetailsDto>> GetByReportIdWithDetailsAsync(Guid id);

    }
}
