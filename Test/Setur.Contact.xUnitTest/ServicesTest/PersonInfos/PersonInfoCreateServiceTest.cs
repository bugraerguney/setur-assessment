using AutoMapper;
using FluentAssertions;
using Moq;
using Setur.Contact.Application.Contracts.Persistance;
using Setur.Contact.Application.Contracts.Persistance.PersonInfos;
using Setur.Contact.Application.Features.PersonInfos;
using Setur.Contact.Application.Features.PersonInfos.Create;
using Setur.Contact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.xUnitTest.ServicesTest.PersonInfos
{
    public class PersonInfoServiceTest
    {
        private readonly Mock<IPersonInfoRepository> _mockRepo;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly PersonInfoService _service;

        public PersonInfoServiceTest()
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
        public async Task CreateAsync_Should_Return_Fail_When_Person_Exists()
        {
            // Arrange
            var request = new CreatePersonInfoRequest("Ali", "Yıldız", "Koç");

            _mockRepo.Setup(r => r.AnyAsync(It.IsAny<Expression<Func<PersonInfo, bool>>>()))
              .ReturnsAsync(true);

            // Act
            var result = await _service.CreateAsync(request);

            // Assert
            result.IsFail.Should().BeTrue();
            result.Status.Should().Be(HttpStatusCode.Conflict);
        }

        [Fact]
        public async Task CreateAsync_Should_Create_Person_When_Not_Exists()
        {
            // Arrange
            var request = new CreatePersonInfoRequest("Ali", "Yıldız", "Koç");
            var person = new PersonInfo { Id = Guid.NewGuid(), Name = "Ali", Surname = "Yıldız", Company = "Koç" };

            _mockRepo.Setup(r => r.AnyAsync(It.IsAny<Expression<Func<PersonInfo, bool>>>())).ReturnsAsync(false);

            _mockMapper.Setup(m => m.Map<PersonInfo>(request)).Returns(person);

            _mockRepo.Setup(r => r.AddAsync(It.IsAny<PersonInfo>())).Returns(ValueTask.CompletedTask);
            _mockUnitOfWork.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await _service.CreateAsync(request);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().Be(person.Id);
        }
    }
}
