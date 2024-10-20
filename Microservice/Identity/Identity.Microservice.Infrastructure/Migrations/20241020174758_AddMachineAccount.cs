using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Identity.Microservice.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMachineAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MachineAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ServiceName = table.Column<string>(type: "text", nullable: false),
                    ApiKey = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineAccounts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "MachineAccounts",
                columns: new[] { "Id", "ApiKey", "CreatedAt", "ServiceName" },
                values: new object[,]
                {
                    { 1, "9c125824-0c86-4c5e-b623-0765d998b441", new DateTime(2024, 10, 20, 17, 47, 57, 431, DateTimeKind.Utc).AddTicks(4709), "Cart.Microservice" },
                    { 2, "c54c4162-d3ac-4297-b77d-89d888a2d688", new DateTime(2024, 10, 20, 17, 47, 57, 431, DateTimeKind.Utc).AddTicks(4712), "Catalog.Microservice" },
                    { 3, "c9035c54-7b64-4980-8a13-e11213ad2b9f", new DateTime(2024, 10, 20, 17, 47, 57, 431, DateTimeKind.Utc).AddTicks(4714), "Orders.Microservice" },
                    { 4, "9aba0313-89bd-4aff-85c8-194ebc5e51dd", new DateTime(2024, 10, 20, 17, 47, 57, 431, DateTimeKind.Utc).AddTicks(4716), "Notification.Microservice" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MachineAccounts");
        }
    }
}
