using Setur.Contact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.Application.Features.ContactInfos.Update
{
    public record UpdateContactInfoRequest(Guid PersonInfoId, InfoType InfoType, string Content);
    
}
