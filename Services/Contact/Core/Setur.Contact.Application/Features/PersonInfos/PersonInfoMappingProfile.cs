using AutoMapper;
using Setur.Contact.Application.Features.ContactInfos.Create;
using Setur.Contact.Application.Features.ContactInfos.Dtos;
using Setur.Contact.Application.Features.ContactInfos.Update;
using Setur.Contact.Application.Features.PersonInfos.Create;
using Setur.Contact.Application.Features.PersonInfos.Dtos;
using Setur.Contact.Application.Features.PersonInfos.Update;
using Setur.Contact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.Application.Features.PersonInfos
{
    public class PersonInfoMappingProfile:Profile
    {
        public PersonInfoMappingProfile()
        {
            CreateMap<ResultPersonInfoDto, PersonInfo>()
               .ForMember(dest => dest.ContactInfos, opt => opt.Ignore())
               .ForMember(dest => dest.Created, opt => opt.Ignore())
               .ForMember(dest => dest.Updated, opt => opt.Ignore())
               .ReverseMap();

             CreateMap<CreatePersonInfoRequest, PersonInfo>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ContactInfos, opt => opt.Ignore())
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.Updated, opt => opt.Ignore())
                .ReverseMap();

             CreateMap<UpdatePersonInfoRequest, PersonInfo>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ContactInfos, opt => opt.Ignore())
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.Updated, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<PersonInfo, ResultPersonWithContactInfosDto>()
    .ForMember(dest => dest.ContactInfos, opt => opt.MapFrom(src => src.ContactInfos))
    .ReverseMap();
        }
    }
}
