using AutoMapper;
using FluentAssertions;
using Moq;
using Setur.Contact.Application.Contracts.Persistance;
using Setur.Contact.Application.Contracts.Persistance.PersonInfos;
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
    public class PersonInfoGetAllListServiceTest
    {
        private readonly Mock<IPersonInfoRepository> _mockRepo;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly PersonInfoService _service;

        public PersonInfoGetAllListServiceTest()
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
        public async Task GetAllListAsync_Should_Return_All_PersonInfos()
        {
            // Arrange
            var people = new List<PersonInfo>
            {
                new PersonInfo { Id = Guid.NewGuid(), Name = "Ali", Surname = "Yıldız", Company = "Koç" },
                new PersonInfo { Id = Guid.NewGuid(), Name = "Veli", Surname = "Kaya", Company = "Arçelik" }
            };

            var mapped = new List<ResultPersonInfoDto>
            {
                new ResultPersonInfoDto(Guid.NewGuid(), "Ali", "Yıldız", "Koç"),
                new ResultPersonInfoDto(Guid.NewGuid(), "Veli", "Kaya", "Arçelik")
            };

            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(people);
            _mockMapper.Setup(m => m.Map<List<ResultPersonInfoDto>>(people)).Returns(mapped);

            // Act
            var result = await _service.GetAllListAsync();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().BeEquivalentTo(mapped);
            result.Status.Should().Be(HttpStatusCode.OK);
        }
    }
}
