using AutoMapper;
using Setur.Report.Application.Features.ReportContacts.Dtos;
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
            CreateMap<ResultReportContactDto, ReportContact>().ReverseMap();
            CreateMap<ResultReportWithDetailsDto, ReportContact>().ReverseMap();

        }

    }
}
