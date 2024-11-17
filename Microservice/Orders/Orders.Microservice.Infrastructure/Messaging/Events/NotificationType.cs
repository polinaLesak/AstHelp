using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Microservice.Infrastructure.Messaging.Events
{
    public enum NotificationType
    {
        Success,
        Warning,
        Error,
        Info
    }
}
