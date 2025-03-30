using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Setur.Report.Application.Features.ReportContacts;

namespace Setur.Report.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportContactsController(IReportContactService  reportContactService) : CustomBaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateReportContact()
        {
            return CreateActionResult(await reportContactService.CreateAsync());
        }
        [HttpGet]
        public async Task<IActionResult> GetReports()
        {
            return CreateActionResult(await reportContactService.GetAllListAsync());
        }
        [HttpGet("GetReportWithDetails/{id}")]
        public async Task<IActionResult> GetReportWithDetails(Guid id)
        {
            return CreateActionResult(await reportContactService.GetByReportIdWithDetailsAsync(id));
        }
    }
}
