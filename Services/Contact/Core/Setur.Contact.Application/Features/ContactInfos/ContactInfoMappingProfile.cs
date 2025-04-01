using AutoMapper;
using Setur.Contact.Application.Features.ContactInfos.Create;
using Setur.Contact.Application.Features.ContactInfos.Dtos;
using Setur.Contact.Application.Features.ContactInfos.Update;
using Setur.Contact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.Application.Features.ContactInfos
{
    public class ContactInfoMappingProfile:Profile
    {
        public ContactInfoMappingProfile()
        {
             CreateMap<ResultContactInfoDto, ContactInfo>()
                .ForMember(dest => dest.PersonInfo, opt => opt.Ignore())
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.Updated, opt => opt.Ignore())
                .ReverseMap();

             CreateMap<CreateContactInfoRequest, ContactInfo>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PersonInfo, opt => opt.Ignore())
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.Updated, opt => opt.Ignore())
                .ReverseMap();

             CreateMap<UpdateContactInfoRequest, ContactInfo>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PersonInfo, opt => opt.Ignore())
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.Updated, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
