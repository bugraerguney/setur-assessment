using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Report.Application.Features.Services
{
    public interface IRabbitMQClientService
    {
        IModel Connect();

    }
}
