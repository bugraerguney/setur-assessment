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
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.xUnitTest.ServicesTest.ContactInfos
{
    public class ContactInfoGetByIdServiceTest
    {
        private readonly Mock<IContactInfoRepository> _mockContactInfoRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly ContactInfoService _contactInfoService;

        public ContactInfoGetByIdServiceTest()
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
        public async Task GetByIdAsync_Should_Return_Result_When_ContactInfo_Exists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var contactInfo = new ContactInfo
            {
                Id = id,
                InfoType = InfoType.Phone,
                Content = "+905555555555",
                PersonInfoId = Guid.NewGuid()
            };

            _mockContactInfoRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(contactInfo);

            // Act
            var result = await _contactInfoService.GetByIdAsync(id);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data!.Id.Should().Be(id);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Success_With_Null_When_ContactInfo_Not_Found()
        {
            // Arrange
            var id = Guid.NewGuid();

            _mockContactInfoRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((ContactInfo?)null);

            // Act
            var result = await _contactInfoService.GetByIdAsync(id);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().BeNull();
        }
    }
}
