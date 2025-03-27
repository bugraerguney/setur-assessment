using Setur.Contact.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.Domain.Entities
{
    public class PersonInfo : BaseEntity<Guid>, IAuditEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public ICollection<ContactInfo> ContactInfos { get; set; } = new List<ContactInfo>();
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
