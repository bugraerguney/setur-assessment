using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.Application.Features.PersonInfos.Create
{
    public record CreatePersonInfoRequest(string Name,string Surname,string Company);
    
}
