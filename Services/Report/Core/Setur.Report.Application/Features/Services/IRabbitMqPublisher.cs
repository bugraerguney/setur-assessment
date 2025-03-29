using Setur.Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Report.Application.Features.Services
{
    public interface IRabbitMqPublisher
    {
        Task PublishAsync(ReportRequestedMessage reportRequestedMessage);
    }
}
