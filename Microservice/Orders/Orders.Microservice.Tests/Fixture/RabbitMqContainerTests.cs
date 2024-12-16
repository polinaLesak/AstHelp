using Orders.Microservice.Tests.Configuration;
using RabbitMQ.Client;

namespace Orders.Microservice.Tests.Fixture;

public class RabbitMqContainerTests(RabbitMqContainerFixture fixture)
    : IClassFixture<RabbitMqContainerFixture>
{
    [Fact]
    public void TestConnectionToRabbitMq()
    {
        var connectionFactory = new ConnectionFactory
        {
            Uri = new Uri(fixture.RabbitMqConnectionString)
        };

        using var connection = connectionFactory.CreateConnection();

        Assert.True(connection.IsOpen);
    }
}