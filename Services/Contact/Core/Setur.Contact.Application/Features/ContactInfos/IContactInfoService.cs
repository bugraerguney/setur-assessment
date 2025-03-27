using Setur.Contact.Application.Features.ContactInfos.Create;
using Setur.Contact.Application.Features.ContactInfos.Dtos;
using Setur.Contact.Application.Features.ContactInfos.Update;
using Setur.Contact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.Application.Features.ContactInfos
{
    public interface IContactInfoService
    {
        Task<ServiceResult<List<ResultContactInfoDto>>> GetAllListAsync();
        Task<ServiceResult<ResultContactInfoDto>> GetByIdAsync(Guid id);
        Task<ServiceResult<Guid>> CreateAsync(CreateContactInfoRequest request);
        Task<ServiceResult> UpdateAsync(Guid id, UpdateContactInfoRequest request);
        Task<ServiceResult> DeleteAsync(Guid id);
    }
}
