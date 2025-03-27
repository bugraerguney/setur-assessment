using Setur.Contact.Application.Features.ContactInfos.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.Application.Features.PersonInfos.Dtos
{
    public record ResultPersonWithContactInfosDto(Guid Id, string Name, string Surname, string Company, List<ResultContactInfoDto> ContactInfos);
     
}
