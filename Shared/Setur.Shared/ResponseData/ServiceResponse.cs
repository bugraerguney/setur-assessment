using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Shared.ResponseData
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
