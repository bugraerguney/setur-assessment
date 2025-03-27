using Setur.Contact.Application.Contracts.Persistance;
using Setur.Contact.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.Persistance
{
    public class UnitOfWork(ContactDbContext context) : IUnitOfWork
    {
        public  Task<int> SaveChangesAsync()
        {
           return  context.SaveChangesAsync();
        }
    }
}
