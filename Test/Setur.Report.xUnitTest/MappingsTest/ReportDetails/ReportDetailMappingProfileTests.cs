using AutoMapper;
using Setur.Report.Application.Features.ReportDetails;
using Setur.Report.Application.Features.ReportDetails.Dtos;
using Setur.Report.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Report.xUnitTest.MappingsTest.ReportDetails
{
    public class ReportDetailMappingProfileTests
    {
        private readonly IMapper _mapper;

        public ReportDetailMappingProfileTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ReportDetailMappingProfile>();
            });

            _mapper = config.CreateMapper();
        }

        [Fact]
        public void Should_Map_ReportDetail_To_ReportDetailDto()
        {
            var entity = new ReportDetail
            {
                Id = Guid.NewGuid(),
                ReportId = Guid.NewGuid(),
                Location = "İstanbul",
                PersonCount = 15,
                PhoneNumberCount = 8
            };

            var dto = _mapper.Map<ReportDetailDto>(entity);

            Assert.Equal(entity.Id, dto.Id);
            Assert.Equal(entity.ReportId, dto.ReportId);
            Assert.Equal(entity.Location, dto.Location);
            Assert.Equal(entity.PersonCount, dto.PersonCount);
            Assert.Equal(entity.PhoneNumberCount, dto.PhoneNumberCount);
        }

        [Fact]
        public void Should_Map_ReportDetailDto_To_ReportDetail()
        {
            var dto = new ReportDetailDto(
                Id: Guid.NewGuid(),
                ReportId: Guid.NewGuid(),
                Location: "Ankara",
                PersonCount: 10,
                PhoneNumberCount: 4
            );

            var entity = _mapper.Map<ReportDetail>(dto);

            Assert.Equal(dto.Id, entity.Id);
            Assert.Equal(dto.ReportId, entity.ReportId);
            Assert.Equal(dto.Location, entity.Location);
            Assert.Equal(dto.PersonCount, entity.PersonCount);
            Assert.Equal(dto.PhoneNumberCount, entity.PhoneNumberCount);
        }

        [Fact]
        public void Mapper_Configuration_IsValid()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ReportDetailMappingProfile>();
            });

            config.AssertConfigurationIsValid();
        }
    }
}
