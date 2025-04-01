using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Setur.Contact.Application.Features.PersonInfos;
using Setur.Contact.Application.Features.PersonInfos.Create;
using Setur.Contact.Application.Features.PersonInfos.Update;
using Setur.Contact.Domain.Entities;
using Setur.Contact.WebApi.Filters;

namespace Setur.Contact.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonInfosController(IPersonInfoService  PersonInfoService) : CustomBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetPersonInfos()
        {
            return CreateActionResult(await PersonInfoService.GetAllListAsync());
        }
        [ServiceFilter(typeof(NotFoundFilter<PersonInfo, Guid>))]

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonInfo(Guid id)
        {
            return CreateActionResult(await PersonInfoService.GetByIdAsync(id));
        }
        [HttpGet("GetPersonWithContactInfos/{id}")]
        public async Task<IActionResult> GetPersonWithContactInfosAsync(Guid id)
        {
            return CreateActionResult(await PersonInfoService.GetPersonWithContactInfosAsync(id));
        }
        [HttpGet("GetPersonStatistics")]
        public async Task<IActionResult> GetPersonStatisticsAsync()
        {
            return CreateActionResult(await PersonInfoService.GetPersonStatisticsAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreatePersonInfo(CreatePersonInfoRequest request)
        {
            return CreateActionResult(await PersonInfoService.CreateAsync(request));
        }
        [ServiceFilter(typeof(NotFoundFilter<PersonInfo, Guid>))]

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePersonInfo(Guid id, UpdatePersonInfoRequest request)
        {
            return CreateActionResult(await PersonInfoService.UpdateAsync(id, request));
        }
        [ServiceFilter(typeof(NotFoundFilter<PersonInfo, Guid>))]

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonInfo(Guid id)
        {
            return CreateActionResult(await PersonInfoService.DeleteAsync(id));
        }
    }
}
