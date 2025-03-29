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
    }
}
