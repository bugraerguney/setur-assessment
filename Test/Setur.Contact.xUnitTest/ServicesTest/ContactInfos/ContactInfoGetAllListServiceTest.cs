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
    public class ContactInfoGetAllListServiceTest
    {
        private readonly Mock<IContactInfoRepository> _mockContactInfoRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly ContactInfoService _contactInfoService;

        public ContactInfoGetAllListServiceTest()
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
        public async Task GetAllListAsync_Should_Return_List_Of_ContactInfos()
        {
            // Arrange
            var contactInfos = new List<ContactInfo>
        {
            new() { Id = Guid.NewGuid(), InfoType = InfoType.Email, Content = "test@test.com", PersonInfoId = Guid.NewGuid() },
            new() { Id = Guid.NewGuid(), InfoType = InfoType.Phone, Content = "+905555555555", PersonInfoId = Guid.NewGuid() }
        };

            _mockContactInfoRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(contactInfos);

            // Act
            var result = await _contactInfoService.GetAllListAsync();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data!.Count.Should().Be(2);
            result.Data![0].Content.Should().Be("test@test.com");
        }
    }
}
