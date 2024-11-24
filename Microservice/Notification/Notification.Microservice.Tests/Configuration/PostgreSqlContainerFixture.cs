using Testcontainers.PostgreSql;

namespace Notification.Microservice.Tests.Configuration;

public class PostgreSqlContainerFixture : IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgres = new PostgreSqlBuilder()
        .WithImage("postgres:17.0-alpine3.20")
        .WithDatabase("TestDb")
        .WithUsername("postgres")
        .WithPassword("testPassword")
        .Build();

    public string ConnectionString => _postgres.GetConnectionString();

    public Task InitializeAsync()
    {
        return _postgres.StartAsync();
    }

    public Task DisposeAsync()
    {
        return _postgres.DisposeAsync().AsTask();
    }
}