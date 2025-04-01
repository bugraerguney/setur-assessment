using AutoMapper;
using Setur.Report.Application.Features.ReportContacts;
using Setur.Report.Application.Features.ReportContacts.Dtos;
using Setur.Report.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Report.xUnitTest.MappingsTest.ReportContacts
{
    public class ReportContactMappingProfileTests
    {
        private readonly IMapper _mapper;

        public ReportContactMappingProfileTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ReportContactMappingProfile>();
            });

            _mapper = config.CreateMapper();
        }

        [Fact]
        public void Should_Map_ReportContact_To_ResultReportContactDto()
        {
            var report = new ReportContact
            {
                Id = Guid.NewGuid(),
                RequestedAt = DateTime.UtcNow,
                CompletedAt = DateTime.UtcNow.AddMinutes(10),
                Status = ReportStatus.Completed
            };

            var dto = _mapper.Map<ResultReportContactDto>(report);

            Assert.Equal(report.Id, dto.Id);
            Assert.Equal(report.RequestedAt, dto.RequestedAt);
            Assert.Equal(report.CompletedAt, dto.CompletedAt);
            Assert.Equal(report.Status, dto.Status);
        }

        [Fact]
        public void Should_Map_ReportContact_With_Details_To_ResultReportWithDetailsDto()
        {
            var report = new ReportContact
            {
                Id = Guid.NewGuid(),
                RequestedAt = DateTime.UtcNow,
                CompletedAt = DateTime.UtcNow.AddMinutes(15),
                Status = ReportStatus.Completed,
                Details = new List<ReportDetail>
                {
                    new ReportDetail
                    {
                        Id = Guid.NewGuid(),
                        Location = "Istanbul",
                        PhoneNumberCount = 5,
                        PersonCount = 3
                    }
                }
            };

            var dto = _mapper.Map<ResultReportWithDetailsDto>(report);

            Assert.Equal(report.Id, dto.Id);
            Assert.Equal(report.Status, dto.Status);
            Assert.Equal(report.Details.Count, dto.Details.Count);
            Assert.Equal(report.Details[0].Location, dto.Details[0].Location);
        }

        [Fact]
        public void Mapper_Configuration_Is_Valid()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ReportContactMappingProfile>();
            });

            config.AssertConfigurationIsValid();
        }
    }
}
