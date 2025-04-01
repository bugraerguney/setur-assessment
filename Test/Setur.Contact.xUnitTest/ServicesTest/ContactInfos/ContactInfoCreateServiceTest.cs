using AutoMapper;
using FluentAssertions;
using Moq;
using Setur.Contact.Application.Contracts.Persistance;
using Setur.Contact.Application.Contracts.Persistance.ContactInfos;
using Setur.Contact.Application.Features.ContactInfos;
using Setur.Contact.Application.Features.ContactInfos.Create;
using Setur.Contact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.xUnitTest.ServicesTest.ContactInfos
{
    public class ContactInfoCreateServiceTest
    {
        private readonly Mock<IContactInfoRepository> _contactInfoRepoMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ContactInfoService _service;

        public ContactInfoCreateServiceTest()
        {
            _contactInfoRepoMock = new Mock<IContactInfoRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();

            _service = new ContactInfoService(
                _contactInfoRepoMock.Object,
                _unitOfWorkMock.Object,
                _mapperMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnConflict_WhenContactInfoExists()
        {
            // Arrange
            var request = new CreateContactInfoRequest(Guid.NewGuid(), InfoType.Email, "test@example.com");

            _contactInfoRepoMock
                .Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<ContactInfo, bool>>>()))
                .ReturnsAsync(true); // Zaten var

            // Act
            var result = await _service.CreateAsync(request);

            // Assert
            result.IsFail.Should().BeTrue();
            result.Status.Should().Be(HttpStatusCode.Conflict);
            result.ErrorMessage.Should().Contain("İletişim Bilgisi dbde var");

            _contactInfoRepoMock.Verify(repo => repo.AddAsync(It.IsAny<ContactInfo>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnCreated_WhenContactInfoIsNew()
        {
            // Arrange
            var request = new CreateContactInfoRequest(Guid.NewGuid(), InfoType.Email, "test@example.com");
            var newContactInfo = new ContactInfo { Id = Guid.NewGuid() };

            _contactInfoRepoMock
                .Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<ContactInfo, bool>>>()))
                .ReturnsAsync(false); // Veri yok

            _mapperMock
                .Setup(m => m.Map<ContactInfo>(request))
                .Returns(newContactInfo);

            // Act
            var result = await _service.CreateAsync(request);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Status.Should().Be(HttpStatusCode.Created);
            result.Data.Should().Be(newContactInfo.Id);

            _contactInfoRepoMock.Verify(repo => repo.AddAsync(It.IsAny<ContactInfo>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }
    }
}
