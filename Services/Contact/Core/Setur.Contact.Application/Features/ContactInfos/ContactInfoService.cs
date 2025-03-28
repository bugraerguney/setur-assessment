using AutoMapper;
using Setur.Contact.Application.Contracts.Persistance;
using Setur.Contact.Application.Contracts.Persistance.ContactInfos;
using Setur.Contact.Application.Features.ContactInfos.Create;
using Setur.Contact.Application.Features.ContactInfos.Dtos;
using Setur.Contact.Application.Features.ContactInfos.Update;
using Setur.Contact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.Application.Features.ContactInfos
{
    public class ContactInfoService(IContactInfoRepository contactInfoRepository, IUnitOfWork unitOfWork, IMapper mapper) : IContactInfoService
    {
        public async Task<ServiceResult<Guid>> CreateAsync(CreateContactInfoRequest request)
        {
            var contactInfo = await contactInfoRepository.AnyAsync(x => x.InfoType == request.InfoType && x.Content==request.Content && x.PersonInfoId==request.PersonInfoId);

            if (contactInfo)
            {
                return ServiceResult<Guid>.Fail("İletişim Bilgisi dbde var", HttpStatusCode.Conflict);
            }
            var newContactInfo = mapper.Map<ContactInfo>(request);
            await contactInfoRepository.AddAsync(newContactInfo);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult<Guid>.SuccessAsCreated(newContactInfo.Id, $"api/contactinfos/{newContactInfo.Id}");
        }

        public async Task<ServiceResult> DeleteAsync(Guid id)
        {
            var contactInfo = await contactInfoRepository.GetByIdAsync(id);


            contactInfoRepository.Delete(contactInfo!);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult<List<ResultContactInfoDto>>> GetAllListAsync()
        {
            var contactInfos = await contactInfoRepository.GetAllAsync();

            var contactInfosDto = mapper.Map<List<ResultContactInfoDto>>(contactInfos);

            return ServiceResult<List<ResultContactInfoDto>>.Success(contactInfosDto);
        }

        public async Task<ServiceResult<ResultContactInfoDto>> GetByIdAsync(Guid id)
        {
            var contactInfo = await contactInfoRepository.GetByIdAsync(id);

            var contactInfoDto = mapper.Map<ResultContactInfoDto>(contactInfo);

            return ServiceResult<ResultContactInfoDto>.Success(contactInfoDto)!;
        }

        public async Task<ServiceResult> UpdateAsync(Guid id, UpdateContactInfoRequest request)
        {
            var contactInfo = await contactInfoRepository.AnyAsync(x => x.InfoType == request.InfoType && x.Content == request.Content && x.PersonInfoId == request.PersonInfoId && x.Id != id);

            if (contactInfo)
            {
                return ServiceResult.Fail("Girdiğiniz iletişim bilgileri veritabanında bulunmaktadır", HttpStatusCode.Conflict);
            }
           
            var newContactInfo = mapper.Map<ContactInfo>(request);
            newContactInfo.Id = id;

            contactInfoRepository.Update(newContactInfo);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }
    }
}
