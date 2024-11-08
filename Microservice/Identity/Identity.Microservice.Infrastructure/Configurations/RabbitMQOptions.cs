﻿namespace Identity.Microservice.Infrastructure.Configurations
{
    public class RabbitMQOptions
    {
        public string HostName { get; set; } = "localhost";
        public int Port { get; set; } = 5672;
        public string UserName { get; set; } = "guest";
        public string Password { get; set; } = "guest";
        public string QueueName { get; set; } = "IdentityQueue";
        public string IdentityExchangeName { get; set; } = "IdentityExchange";
    }
}