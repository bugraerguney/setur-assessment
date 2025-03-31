using AutoMapper;
using Setur.Report.Application.Features.ReportContacts.Dtos;
using Setur.Report.Application.Features.ReportDetails.Dtos;
using Setur.Report.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Report.Application.Features.ReportContacts
{
    public class ReportContactMappingProfile:Profile
    {
        public ReportContactMappingProfile()
        {
             CreateMap<ResultReportContactDto, ReportContact>()
                .ForMember(dest => dest.Details, opt => opt.Ignore())
                .ReverseMap();

             CreateMap<ResultReportWithDetailsDto, ReportContact>()
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.Details))
                .ReverseMap()
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.Details));

            CreateMap<ReportDetail, ReportDetailDto>()
           .ReverseMap()
           .ForMember(dest => dest.Report, opt => opt.Ignore());
        }

    }
}
