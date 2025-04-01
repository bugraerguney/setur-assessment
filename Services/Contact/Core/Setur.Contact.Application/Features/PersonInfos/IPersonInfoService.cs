using Setur.Contact.Application.Features.ContactInfos.Create;
using Setur.Contact.Application.Features.ContactInfos.Dtos;
using Setur.Contact.Application.Features.ContactInfos.Update;
using Setur.Contact.Application.Features.PersonInfos.Create;
using Setur.Contact.Application.Features.PersonInfos.Dtos;
using Setur.Contact.Application.Features.PersonInfos.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.Application.Features.PersonInfos
{
    public interface IPersonInfoService
    {
        Task<ServiceResult<List<ResultPersonInfoDto>>> GetAllListAsync();
        Task<ServiceResult<ResultPersonInfoDto>> GetByIdAsync(Guid id);
        Task<ServiceResult<Guid>> CreateAsync(CreatePersonInfoRequest request);
        Task<ServiceResult> UpdateAsync(Guid id, UpdatePersonInfoRequest request);
        Task<ServiceResult> DeleteAsync(Guid id);
        Task<ServiceResult<ResultPersonWithContactInfosDto>> GetPersonWithContactInfosAsync(Guid id);
        Task<ServiceResult<List<ResultPersonStatisticDto>>> GetPersonStatisticsAsync();

     }
}
