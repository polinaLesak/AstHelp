using DotNet.Testcontainers.Builders;
using Testcontainers.RabbitMq;

namespace Cart.Microservice.Tests.Integration.Configuration
{
    public class RabbitMqContainerFixture : IAsyncLifetime
    {
        private RabbitMqContainer _rabbitMq;

        public string RabbitMqConnectionString => _rabbitMq.GetConnectionString();

        public RabbitMqContainerFixture()
        {
            _rabbitMq = new RabbitMqBuilder()
                .WithImage("rabbitmq:3-management")
                .WithUsername("user")
                .WithPassword("password")
                .WithExposedPort(5672)
                .WithExposedPort(15672)
                .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5672))
                .Build();
        }

        public Task InitializeAsync()
        {
            return _rabbitMq.StartAsync();
        }

        public Task DisposeAsync()
        {
            return _rabbitMq.DisposeAsync().AsTask();
        }
    }
}
