using Microsoft.EntityFrameworkCore;
using Setur.Contact.Application.Contracts.Persistance.ContactInfos;
using Setur.Contact.Application.Contracts.Persistance.PersonInfos;
using Setur.Contact.Domain.Entities;
using Setur.Contact.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.Persistance.PersonInfos
{
    public class PersonInfoRepository(ContactDbContext context) : GenericRepository<PersonInfo, Guid>(context), IPersonInfoRepository
    {
        public async Task<PersonInfo?> GetPersonWithContactInfosAsync(Guid id)
        {
            return await Context.PersonInfos.AsNoTracking()
        .Include(p => p.ContactInfos)
        .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
