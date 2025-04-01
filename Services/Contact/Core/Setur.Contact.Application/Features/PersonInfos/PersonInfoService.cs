using AutoMapper;
using Setur.Contact.Application.Contracts.Persistance.ContactInfos;
using Setur.Contact.Application.Contracts.Persistance;
using Setur.Contact.Application.Features.PersonInfos.Create;
using Setur.Contact.Application.Features.PersonInfos.Dtos;
using Setur.Contact.Application.Features.PersonInfos.Update;
using Setur.Contact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Setur.Contact.Application.Features.ContactInfos;
using Setur.Contact.Application.Contracts.Persistance.PersonInfos;
using Setur.Contact.Application.Features.ContactInfos.Dtos;

namespace Setur.Contact.Application.Features.PersonInfos
{
    public class PersonInfoService(IPersonInfoRepository  personInfoRepository, IUnitOfWork unitOfWork, IMapper mapper) : IPersonInfoService
    {
        public async Task<ServiceResult<Guid>> CreateAsync(CreatePersonInfoRequest request)
        {
            var contactInfo = await personInfoRepository.AnyAsync(x => x.Name == request.Name && x.Surname==request.Surname && x.Company == request.Company);

            if (contactInfo)
            {
                return ServiceResult<Guid>.Fail("Kişi Bilgisi dbde var", HttpStatusCode.Conflict);
            }
            var newContactInfo = mapper.Map<PersonInfo>(request);
            await personInfoRepository.AddAsync(newContactInfo);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult<Guid>.SuccessAsCreated(newContactInfo.Id, $"api/personinfos/{newContactInfo.Id}");
        }

        public async Task<ServiceResult> DeleteAsync(Guid id)
        {
            var personInfo = await personInfoRepository.GetByIdAsync(id);


            personInfoRepository.Delete(personInfo!);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult<List<ResultPersonInfoDto>>> GetAllListAsync()
        {
            var personInfos = await personInfoRepository.GetAllAsync();

            var personInfosDto = mapper.Map<List<ResultPersonInfoDto>>(personInfos);

            return ServiceResult<List<ResultPersonInfoDto>>.Success(personInfosDto);
        }

        public async Task<ServiceResult<ResultPersonInfoDto>> GetByIdAsync(Guid id)
        {
            var personInfo = await personInfoRepository.GetByIdAsync(id);

            if (personInfo is null)
            {
                return ServiceResult<ResultPersonInfoDto>.Fail("Kişi bilgisi bulunamadı", HttpStatusCode.NotFound);
            }
            var personInfoDto = mapper.Map<ResultPersonInfoDto>(personInfo);

            return ServiceResult<ResultPersonInfoDto>.Success(personInfoDto)!;
        }

        public async Task<ServiceResult<List<ResultPersonStatisticDto>>> GetPersonStatisticsAsync()
        {
            var statistics= await personInfoRepository.GetPersonStatisticsAsync();
            return ServiceResult<List<ResultPersonStatisticDto>>.Success(statistics);

        }

        public async Task<ServiceResult<ResultPersonWithContactInfosDto>> GetPersonWithContactInfosAsync(Guid id)
        {

            var person = await personInfoRepository.GetPersonWithContactInfosAsync(id);

            var personDto = mapper.Map<ResultPersonWithContactInfosDto>(person);
             return ServiceResult<ResultPersonWithContactInfosDto>.Success(personDto);

        }

        public async Task<ServiceResult> UpdateAsync(Guid id, UpdatePersonInfoRequest request)
        {
            var personInfo = await personInfoRepository.AnyAsync( x => x.Name == request.Name && x.Surname == request.Surname && x.Company == request.Company && x.Id != id);

            if (personInfo)
            {
                return ServiceResult.Fail("Girdiğiniz kişi bilgileri veritabanında bulunmaktadır", HttpStatusCode.Conflict);
            }
          
            var newPersonInfo = mapper.Map<PersonInfo>(request);
            newPersonInfo.Id = id;

            personInfoRepository.Update(newPersonInfo);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }
    }
}
