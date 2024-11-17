using Microsoft.Extensions.Options;
using Notification.Microservice.Infrastructure.Messaging;
using Notification.Microservice.Infrastructure.Messaging.Configurations;
using RabbitMQ.Client;

namespace Notification.Microservice.API.Configuration
{
    public static class RabbitMqConfiguration
    {
        public static IServiceCollection ConfigureRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMQOptions>(configuration.GetSection("RabbitMQ"));

            services.AddSingleton(sp =>
            {
                var options = sp.GetRequiredService<IOptions<RabbitMQOptions>>().Value;
                var factory = new ConnectionFactory()
                {
                    HostName = options.HostName,
                    Port = options.Port,
                    UserName = options.UserName,
                    Password = options.Password
                };
                return factory.CreateConnection();
            });

            services.AddSingleton(sp =>
            {
                var connection = sp.GetRequiredService<IConnection>();
                return connection.CreateModel();
            });

            services.AddHostedService<RabbitMQConsumer>();

            return services;
        }
    }
}
