using Notification.Microservice.Tests.Integration.Configuration;
using RabbitMQ.Client;

namespace Notification.Microservice.Tests.Integration
{
    public class RabbitMqContainerTests : IClassFixture<RabbitMqContainerFixture>
    {
        private readonly RabbitMqContainerFixture _fixture;

        public RabbitMqContainerTests(RabbitMqContainerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void TestConnectionToRabbitMq()
        {
            var connectionFactory = new ConnectionFactory();
            connectionFactory.Uri = new Uri(_fixture.RabbitMqConnectionString);

            using var connection = connectionFactory.CreateConnection();

            Assert.True(connection.IsOpen);
        }
    }
}
