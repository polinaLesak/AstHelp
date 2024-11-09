using Testcontainers.PostgreSql;

namespace Notification.Microservice.Tests.Integration.Configuration
{
    public class PostgreSqlContainerFixture : IAsyncLifetime
    {
        private PostgreSqlContainer _postgres;

        public string ConnectionString => _postgres.GetConnectionString();

        public PostgreSqlContainerFixture()
        {
            _postgres = new PostgreSqlBuilder()
                .WithImage("postgres:17.0-alpine3.20")
                .WithDatabase("TestDb")
                .WithUsername("postgres")
                .WithPassword("testpassword")
                .Build();
        }

        public Task InitializeAsync()
        {
            return _postgres.StartAsync();
        }

        public Task DisposeAsync()
        {
            return _postgres.DisposeAsync().AsTask();
        }
    }
}
