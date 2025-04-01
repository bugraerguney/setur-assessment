using AutoMapper;
using FluentAssertions;
using Moq;
using Setur.Contact.Application.Contracts.Persistance;
using Setur.Contact.Application.Contracts.Persistance.PersonInfos;
using Setur.Contact.Application.Features.PersonInfos;
using Setur.Contact.Application.Features.PersonInfos.Update;
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
    public class PersonInfoUpdateServiceTest
    {
        private readonly Mock<IPersonInfoRepository> _mockRepo;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly PersonInfoService _service;

        public PersonInfoUpdateServiceTest()
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
        public async Task UpdateAsync_Should_Return_Fail_When_Duplicate_PersonInfo_Exists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var request = new UpdatePersonInfoRequest("Ali", "Yıldız", "Koç");

            _mockRepo.Setup(r => r.AnyAsync(It.IsAny<Expression<Func<PersonInfo, bool>>>()))
                .ReturnsAsync(true);

            // Act
            var result = await _service.UpdateAsync(id, request);

            // Assert
            result.IsFail.Should().BeTrue();
            result.Status.Should().Be(HttpStatusCode.Conflict);
        }

        [Fact]
        public async Task UpdateAsync_Should_Succeed_When_Valid_Request()
        {
            // Arrange
            var id = Guid.NewGuid();
            var request = new UpdatePersonInfoRequest("Ayşe", "Kaya", "ASELSAN");
            var person = new PersonInfo { Id = id, Name = "Ayşe", Surname = "Kaya", Company = "ASELSAN" };

            _mockRepo.Setup(r => r.AnyAsync(It.IsAny<Expression<Func<PersonInfo, bool>>>()))
                .ReturnsAsync(false);

            _mockMapper.Setup(m => m.Map<PersonInfo>(request)).Returns(person);
            _mockUnitOfWork.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await _service.UpdateAsync(id, request);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Status.Should().Be(HttpStatusCode.NoContent);
            _mockRepo.Verify(r => r.Update(It.Is<PersonInfo>(x => x.Id == id)), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        }
    }
}
