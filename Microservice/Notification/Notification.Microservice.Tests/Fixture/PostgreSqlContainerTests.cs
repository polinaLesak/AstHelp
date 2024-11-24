﻿using Notification.Microservice.Tests.Configuration;

namespace Notification.Microservice.Tests.Fixture;

public class PostgreSqlContainerTests(PostgreSqlContainerFixture fixture)
    : IClassFixture<PostgreSqlContainerFixture>
{
    [Fact]
    public async Task TestConnectionToPostgreSql()
    {
        var connectionString = fixture.ConnectionString;

        await using var connection = new Npgsql.NpgsqlConnection(connectionString);
        await connection.OpenAsync();

        Assert.Equal("TestDb", connection.Database);
    }
}