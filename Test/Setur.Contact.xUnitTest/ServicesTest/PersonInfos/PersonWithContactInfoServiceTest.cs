using AutoMapper;
using FluentAssertions;
using Moq;
using Setur.Contact.Application.Contracts.Persistance;
using Setur.Contact.Application.Contracts.Persistance.PersonInfos;
using Setur.Contact.Application.Features.ContactInfos.Dtos;
using Setur.Contact.Application.Features.PersonInfos;
using Setur.Contact.Application.Features.PersonInfos.Dtos;
using Setur.Contact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.xUnitTest.ServicesTest.PersonInfos
{
    public class PersonWithContactInfoServiceTest
    {
        private readonly Mock<IPersonInfoRepository> _mockRepo;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly PersonInfoService _service;

        public PersonWithContactInfoServiceTest()
        {
            _mockRepo = new Mock<IPersonInfoRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();

            _service = new PersonInfoService(
                _mockRepo.Object,
                _mockUnitOfWork.Object,
                _mockMapper.Object
            );
        }

        [Fact]
        public async Task GetPersonWithContactInfosAsync_Should_Return_Person_With_ContactInfos()
        {
            // Arrange
            var personId = Guid.NewGuid();

            var personEntity = new PersonInfo
            {
                Id = personId,
                Name = "Ali",
                Surname = "Yıldız",
                Company = "Koç",
                ContactInfos = new List<ContactInfo>
                {
                    new() { InfoType = InfoType.Email, Content = "ali@example.com" },
                    new() { InfoType = InfoType.Phone, Content = "+905551234567" }
                }
            };

            var dto = new ResultPersonWithContactInfosDto(
                personId,
                "Ali",
                "Yıldız",
                "Koç",
                new List<ResultContactInfoDto>
                {
                    new(Guid.NewGuid(), personId, InfoType.Email, "ali@example.com"),
        new(Guid.NewGuid(), personId, InfoType.Phone, "+905551234567")
                }
            );

            _mockRepo.Setup(r => r.GetPersonWithContactInfosAsync(personId)).ReturnsAsync(personEntity);
            _mockMapper.Setup(m => m.Map<ResultPersonWithContactInfosDto>(personEntity)).Returns(dto);

            // Act
            var result = await _service.GetPersonWithContactInfosAsync(personId);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data!.ContactInfos.Should().HaveCount(2);
            result.Status.Should().Be(HttpStatusCode.OK);
        }
    }
}
