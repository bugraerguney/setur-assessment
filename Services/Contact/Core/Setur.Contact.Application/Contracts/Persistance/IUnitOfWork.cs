﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.Application.Contracts.Persistance
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();

    }
}
