namespace Orders.Microservice.Infrastructure.Messaging.Configurations
{
    public class RabbitMQOptions
    {
        public string HostName { get; set; } = "localhost";
        public int Port { get; set; } = 5672;
        public string UserName { get; set; } = "guest";
        public string Password { get; set; } = "guest";
        public string QueueName { get; set; } = "OrdersQueue";
        public string ExchangeName { get; set; } = "OrdersExchange";
    }
}
