using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Setur.Contact.Application.Contracts.Persistance.ContactInfos;
using Setur.Contact.Application.Features.ContactInfos;
using Setur.Contact.Application.Features.ContactInfos.Create;
using Setur.Contact.Application.Features.ContactInfos.Update;
using Setur.Contact.Domain.Entities;
using Setur.Contact.WebApi.Filters;

namespace Setur.Contact.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactInfosController(IContactInfoService  ContactInfoService) : CustomBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetContactInfos()
        {
            return CreateActionResult(await ContactInfoService.GetAllListAsync());
        }
        [ServiceFilter(typeof(NotFoundFilter<ContactInfo, Guid>))]

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactInfo(Guid id)
        {
            return CreateActionResult(await ContactInfoService.GetByIdAsync(id));
        }
        

        [HttpPost]
        public async Task<IActionResult> CreateContactInfo(CreateContactInfoRequest request)
        {
            return CreateActionResult(await ContactInfoService.CreateAsync(request));
        }
        [ServiceFilter(typeof(NotFoundFilter<ContactInfo, Guid>))]

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContactInfo(Guid id, UpdateContactInfoRequest request)
        {
            return CreateActionResult(await ContactInfoService.UpdateAsync(id, request));
        }
        [ServiceFilter(typeof(NotFoundFilter<ContactInfo, Guid>))]

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactInfo(Guid id)
        {
            return CreateActionResult(await ContactInfoService.DeleteAsync(id));
        }
    }
}
