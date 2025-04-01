using AutoMapper;
using FluentAssertions;
using Moq;
using Setur.Contact.Application.Contracts.Persistance;
using Setur.Contact.Application.Contracts.Persistance.ContactInfos;
using Setur.Contact.Application.Features.ContactInfos;
using Setur.Contact.Application.Features.ContactInfos.Dtos;
using Setur.Contact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.xUnitTest.ServicesTest.ContactInfos
{
    public class ContactInfoDeleteServiceTest
    {
        private readonly Mock<IContactInfoRepository> _mockContactInfoRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly ContactInfoService _contactInfoService;

        public ContactInfoDeleteServiceTest()
        {
            _mockContactInfoRepository = new Mock<IContactInfoRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ContactInfo, ResultContactInfoDto>();
            });
            _mapper = config.CreateMapper();

            _contactInfoService = new ContactInfoService(
                _mockContactInfoRepository.Object,
                _mockUnitOfWork.Object,
                _mapper
            );
        }

        [Fact]
        public async Task DeleteAsync_Should_Delete_ContactInfo_When_Exists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var existingContact = new ContactInfo
            {
                Id = id,
                InfoType = InfoType.Phone,
                Content = "+905555555555",
                PersonInfoId = Guid.NewGuid()
            };

            _mockContactInfoRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(existingContact);
            _mockUnitOfWork.Setup(uow => uow.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await _contactInfoService.DeleteAsync(id);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Status.Should().Be(HttpStatusCode.NoContent);
            _mockContactInfoRepository.Verify(repo => repo.Delete(existingContact), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(), Times.Once);
        }
    }
}
