namespace Orders.Microservice.Infrastructure.Events
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
