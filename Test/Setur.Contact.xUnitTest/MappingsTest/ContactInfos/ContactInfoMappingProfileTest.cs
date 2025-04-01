using AutoMapper;
using Setur.Contact.Application.Features.ContactInfos;
using Setur.Contact.Application.Features.ContactInfos.Create;
using Setur.Contact.Application.Features.ContactInfos.Dtos;
using Setur.Contact.Application.Features.ContactInfos.Update;
using Setur.Contact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.xUnitTest.MappingsTest.ContactInfos
{
    public class ContactInfoMappingProfileTest
    {
        private readonly IMapper _mapper;

        public ContactInfoMappingProfileTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ContactInfoMappingProfile>();
            });

            config.AssertConfigurationIsValid();  
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void Should_Map_CreateContactInfoRequest_To_ContactInfo()
        {
            var source = new CreateContactInfoRequest(Guid.NewGuid(), InfoType.Email, "test@example.com");

            var result = _mapper.Map<ContactInfo>(source);

            Assert.Equal(source.PersonInfoId, result.PersonInfoId);
            Assert.Equal(source.InfoType, result.InfoType);
            Assert.Equal(source.Content, result.Content);
        }

        [Fact]
        public void Should_Map_UpdateContactInfoRequest_To_ContactInfo()
        {
            var source = new UpdateContactInfoRequest(Guid.NewGuid(), InfoType.Phone, "+905551234567");

            var result = _mapper.Map<ContactInfo>(source);

            Assert.Equal(source.PersonInfoId, result.PersonInfoId);
            Assert.Equal(source.InfoType, result.InfoType);
            Assert.Equal(source.Content, result.Content);
        }

        [Fact]
        public void Should_Map_ContactInfo_To_ResultContactInfoDto()
        {
            var entity = new ContactInfo
            {
                Id = Guid.NewGuid(),
                PersonInfoId = Guid.NewGuid(),
                InfoType = InfoType.Location,
                Content = "Ankara"
            };

            var dto = _mapper.Map<ResultContactInfoDto>(entity);

            Assert.Equal(entity.Id, dto.Id);
            Assert.Equal(entity.PersonInfoId, dto.PersonInfoId);
            Assert.Equal(entity.InfoType, dto.InfoType);
            Assert.Equal(entity.Content, dto.Content);
        }

        [Fact]
        public void Should_Map_ResultContactInfoDto_To_ContactInfo()
        {
            var dto = new ResultContactInfoDto(Guid.NewGuid(), Guid.NewGuid(), InfoType.Email, "mail@test.com");

            var entity = _mapper.Map<ContactInfo>(dto);

            Assert.Equal(dto.Id, entity.Id);
            Assert.Equal(dto.PersonInfoId, entity.PersonInfoId);
            Assert.Equal(dto.InfoType, entity.InfoType);
            Assert.Equal(dto.Content, entity.Content);
        }
    }
}
