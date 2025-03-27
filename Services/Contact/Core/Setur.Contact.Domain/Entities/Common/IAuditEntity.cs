using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.Domain.Entities.Common
{
    public interface IAuditEntity
    {
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
