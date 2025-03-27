using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.Application.Features.PersonInfos.Update
{
    public record UpdatePersonInfoRequest(string Name, string Surname, string Company);
    
}
