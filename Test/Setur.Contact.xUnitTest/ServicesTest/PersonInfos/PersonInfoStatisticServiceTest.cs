using AutoMapper;
using FluentAssertions;
using Moq;
using Setur.Contact.Application.Contracts.Persistance;
using Setur.Contact.Application.Contracts.Persistance.PersonInfos;
using Setur.Contact.Application.Features.PersonInfos;
using Setur.Contact.Application.Features.PersonInfos.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.xUnitTest.ServicesTest.PersonInfos
{
    public class PersonInfoStatisticServiceTest
    {
        private readonly Mock<IPersonInfoRepository> _mockRepo;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly PersonInfoService _service;

        public PersonInfoStatisticServiceTest()
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
        public async Task GetPersonStatisticsAsync_Should_Return_Statistics()
        {
            // Arrange
            var statistics = new List<ResultPersonStatisticDto>
            {
                new() { Location = "İstanbul", PersonCount = 3, PhoneNumberCount = 5 },
                new() { Location = "Ankara", PersonCount = 2, PhoneNumberCount = 3 }
            };

            _mockRepo.Setup(r => r.GetPersonStatisticsAsync())
                     .ReturnsAsync(statistics);

            // Act
            var result = await _service.GetPersonStatisticsAsync();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.Should().HaveCount(2);
            result.Data![0].Location.Should().Be("İstanbul");
            result.Status.Should().Be(HttpStatusCode.OK);
        }
    }
}
