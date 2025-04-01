using Setur.Contact.Application.Contracts.Persistance.ContactInfos;
using Setur.Contact.Domain.Entities;
using Setur.Contact.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.Persistance.ContactInfos
{
    public class ContactInfoRepository(ContactDbContext context) : GenericRepository<ContactInfo, Guid>(context), IContactInfoRepository
    {
        
    }
}
