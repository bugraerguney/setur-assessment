using Setur.Contact.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.Domain.Entities
{
    public class ContactInfo: BaseEntity<Guid>, IAuditEntity
    {
         public Guid PersonInfoId { get; set; }
        public InfoType InfoType { get; set; }
        public string Content { get; set; }
        public PersonInfo PersonInfo { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
