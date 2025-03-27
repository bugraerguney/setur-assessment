using Setur.Contact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.Application.Contracts.Persistance.ContactInfos
{
    public interface IContactInfoRepository : IGenericRepository<ContactInfo, Guid>
    {
    }
}
