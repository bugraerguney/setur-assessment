using AutoMapper;
using FluentAssertions;
using Moq;
using Setur.Contact.Application.Contracts.Persistance;
using Setur.Contact.Application.Contracts.Persistance.PersonInfos;
using Setur.Contact.Application.Features.PersonInfos;
using Setur.Contact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.xUnitTest.ServicesTest.PersonInfos
{
    public class PersonInfoDeleteServiceTest
    {
        private readonly Mock<IPersonInfoRepository> _mockRepo;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly PersonInfoService _service;

        public PersonInfoDeleteServiceTest()
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
        public async Task DeleteAsync_Should_Succeed_When_Person_Exists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var person = new PersonInfo { Id = id };

            _mockRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(person);
            _mockUnitOfWork.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await _service.DeleteAsync(id);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Status.Should().Be(HttpStatusCode.NoContent);
            _mockRepo.Verify(r => r.Delete(person), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        }
    }
}
