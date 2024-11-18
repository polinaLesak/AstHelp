using Catalog.Microservice.Infrastructure.Messaging;
using Catalog.Microservice.Infrastructure.Messaging.Configurations;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Catalog.Microservice.API.Configuration
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

            // Добавляем канал RabbitMQ
            services.AddSingleton(sp =>
            {
                var connection = sp.GetRequiredService<IConnection>();
                return connection.CreateModel();
            });

            services.AddSingleton<RabbitMQProducer>();

            return services;
        }
    }
}
