using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Report.Application
{
    
        public interface IUnitOfWork
        {
            Task<int> SaveChangesAsync();

        }
     
}
