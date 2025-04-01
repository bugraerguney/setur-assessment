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
    public class PersonInfoGetByIdServiceTest
    {
        private readonly Mock<IPersonInfoRepository> _mockRepo;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly PersonInfoService _service;

        public PersonInfoGetByIdServiceTest()
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
        public async Task GetByIdAsync_Should_Return_NotFound_When_Person_Not_Exist()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((PersonInfo?)null);

            // Act
            var result = await _service.GetByIdAsync(id);

            // Assert
            result.IsFail.Should().BeTrue();
            result.Status.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Person_When_Exists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var person = new PersonInfo { Id = id, Name = "Ali", Surname = "Yıldız", Company = "Koç" };
            var mapped = new ResultPersonInfoDto(id, "Ali", "Yıldız", "Koç");

            _mockRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(person);
            _mockMapper.Setup(m => m.Map<ResultPersonInfoDto>(person)).Returns(mapped);

            // Act
            var result = await _service.GetByIdAsync(id);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().Be(mapped);
        }
    }
}
