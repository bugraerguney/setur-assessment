using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.Domain.Options
{
    public class ConnectionStringOption
    {
        public const string Key = "ConnectionStrings";
        public string Npgsql { get; set; } = default!;
    }
}
