using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Microservice.Domain.Events
{
    public class EventWrapper<T>
    {
        public string EventType { get; set; }
        public T Data { get; set; }

        public EventWrapper(string eventType, T data)
        {
            EventType = eventType;
            Data = data;
        }
    }
}
