using AutoMapper;
using FluentAssertions;
using Moq;
using Setur.Contact.Application.Contracts.Persistance;
using Setur.Contact.Application.Contracts.Persistance.ContactInfos;
using Setur.Contact.Application.Features.ContactInfos;
using Setur.Contact.Application.Features.ContactInfos.Update;
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
    public class ContactInfoUpdateServiceTest
    {
        private readonly Mock<IContactInfoRepository> _mockContactInfoRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly ContactInfoService _contactInfoService;

        public ContactInfoUpdateServiceTest()
        {
            _mockContactInfoRepository = new Mock<IContactInfoRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdateContactInfoRequest, ContactInfo>()
                   .ForMember(dest => dest.Id, opt => opt.Ignore());
            });
            _mapper = config.CreateMapper();

            _contactInfoService = new ContactInfoService(
                _mockContactInfoRepository.Object,
                _mockUnitOfWork.Object,
                _mapper
            );
        }

        [Fact]
        public async Task UpdateAsync_Should_Return_Conflict_If_Duplicate_Exists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var request = new UpdateContactInfoRequest(Guid.NewGuid(), InfoType.Email, "test@test.com");

            _mockContactInfoRepository.Setup(x => x.AnyAsync(
                It.IsAny<Expression<Func<ContactInfo, bool>>>())).ReturnsAsync(true);

            // Act
            var result = await _contactInfoService.UpdateAsync(id, request);

            // Assert
            result.IsFail.Should().BeTrue();
            result.Status.Should().Be(HttpStatusCode.Conflict);
        }

        [Fact]
        public async Task UpdateAsync_Should_Update_And_Save_When_Valid()
        {
            // Arrange
            var id = Guid.NewGuid();
            var request = new UpdateContactInfoRequest(Guid.NewGuid(), InfoType.Email, "test@test.com");

            _mockContactInfoRepository.Setup(x => x.AnyAsync(
                It.IsAny<Expression<Func<ContactInfo, bool>>>())).ReturnsAsync(false);

            _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await _contactInfoService.UpdateAsync(id, request);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Status.Should().Be(HttpStatusCode.NoContent);

            _mockContactInfoRepository.Verify(x => x.Update(It.Is<ContactInfo>(c => c.Id == id)), Times.Once);
            _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}
