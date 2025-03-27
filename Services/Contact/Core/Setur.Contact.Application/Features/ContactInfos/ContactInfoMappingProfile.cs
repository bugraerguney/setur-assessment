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
            CreateMap<ResultContactInfoDto,ContactInfo>().ReverseMap();
            CreateMap<CreateContactInfoRequest,ContactInfo>().ReverseMap();
            CreateMap<UpdateContactInfoRequest,ContactInfo>().ReverseMap();
         }
    }
}
