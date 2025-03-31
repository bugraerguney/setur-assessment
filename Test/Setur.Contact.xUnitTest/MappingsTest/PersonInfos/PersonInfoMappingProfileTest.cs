using AutoMapper;
using Setur.Contact.Application.Features.PersonInfos;
using Setur.Contact.Application.Features.PersonInfos.Create;
using Setur.Contact.Application.Features.PersonInfos.Dtos;
using Setur.Contact.Application.Features.PersonInfos.Update;
using Setur.Contact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.xUnitTest.MappingsTest.PersonInfos
{
    public class PersonInfoMappingProfileTest
    {
        private readonly IMapper _mapper;

        public PersonInfoMappingProfileTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<PersonInfoMappingProfile>();
            });

            config.AssertConfigurationIsValid();  
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void Should_Map_CreatePersonInfoRequest_To_PersonInfo()
        {
            var source = new CreatePersonInfoRequest("Ali", "Veli", "Setur");
            var result = _mapper.Map<PersonInfo>(source);

            Assert.Equal(source.Name, result.Name);
            Assert.Equal(source.Surname, result.Surname);
            Assert.Equal(source.Company, result.Company);
        }

        [Fact]
        public void Should_Map_UpdatePersonInfoRequest_To_PersonInfo()
        {
            var source = new UpdatePersonInfoRequest("Ayşe", "Kaya", "Şirket A.Ş.");
            var result = _mapper.Map<PersonInfo>(source);

            Assert.Equal(source.Name, result.Name);
            Assert.Equal(source.Surname, result.Surname);
            Assert.Equal(source.Company, result.Company);
        }

        [Fact]
        public void Should_Map_PersonInfo_To_ResultPersonInfoDto()
        {
            var person = new PersonInfo
            {
                Id = Guid.NewGuid(),
                Name = "Zeynep",
                Surname = "Demir",
                Company = "Tech Co."
            };

            var dto = _mapper.Map<ResultPersonInfoDto>(person);

            Assert.Equal(person.Id, dto.Id);
            Assert.Equal(person.Name, dto.Name);
            Assert.Equal(person.Surname, dto.Surname);
            Assert.Equal(person.Company, dto.Company);
        }

        [Fact]
        public void Should_Map_ResultPersonInfoDto_To_PersonInfo()
        {
            var dto = new ResultPersonInfoDto(Guid.NewGuid(), "Kerem", "Yılmaz", "XYZ Ltd.");
            var result = _mapper.Map<PersonInfo>(dto);

            Assert.Equal(dto.Id, result.Id);
            Assert.Equal(dto.Name, result.Name);
            Assert.Equal(dto.Surname, result.Surname);
            Assert.Equal(dto.Company, result.Company);
        }
    }
}
