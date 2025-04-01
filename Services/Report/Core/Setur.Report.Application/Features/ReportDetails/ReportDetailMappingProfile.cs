using AutoMapper;
using Setur.Report.Application.Features.ReportDetails.Dtos;
using Setur.Report.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Report.Application.Features.ReportDetails
{
    public class ReportDetailMappingProfile : Profile
    {
        public ReportDetailMappingProfile()
        {
            CreateMap<ReportDetail, ReportDetailDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ReverseMap()
                .ForMember(dest => dest.Report, opt => opt.Ignore());  
        }
    }
}
