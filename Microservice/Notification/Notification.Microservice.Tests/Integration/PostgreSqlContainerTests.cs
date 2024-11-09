using Notification.Microservice.Tests.Integration.Configuration;

namespace Notification.Microservice.Tests.Integration
{
    public class PostgreSqlContainerTests : IClassFixture<PostgreSqlContainerFixture>
    {
        private readonly PostgreSqlContainerFixture _fixture;

        public PostgreSqlContainerTests(PostgreSqlContainerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task TestConnectionToPostgreSql()
        {
            var connectionString = _fixture.ConnectionString;

            await using (var connection = new Npgsql.NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();

                Assert.Equal("TestDb", connection.Database);
            }
        }
    }
}
