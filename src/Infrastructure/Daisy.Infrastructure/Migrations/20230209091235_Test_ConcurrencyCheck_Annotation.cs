using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Daisy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TestConcurrencyCheckAnnotation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Token = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IssuedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Token);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "29bf55b3-edd7-4b22-919a-da0e9ad6a0a2");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "d3cdd71d-b21b-44d7-8c3a-db07466c75a7");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "809ab734-4272-4cd0-b2c5-d8651f85216c");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ApprovedOn", "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { new DateTime(2023, 2, 9, 12, 12, 35, 0, DateTimeKind.Local).AddTicks(6224), "d5117913-65af-49a4-8810-858f30d30227", new DateTime(2023, 2, 9, 12, 12, 35, 0, DateTimeKind.Local).AddTicks(6214), "AQAAAAEAACcQAAAAELN85iwV9uRK6u8oWcpgPwsHS77GUJKkPJ7kx1izMZtv0MOq4GLBAeU++bXKiBOO2Q==", "5e57cce0-606a-4d8f-bd5b-d517807f2003" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ApprovedOn", "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { new DateTime(2023, 2, 9, 12, 12, 35, 0, DateTimeKind.Local).AddTicks(6230), "2ccc38a5-7349-420c-8310-1950fa0dd361", new DateTime(2023, 2, 9, 12, 12, 35, 0, DateTimeKind.Local).AddTicks(6229), "AQAAAAEAACcQAAAAEFjjc8wyJtgXVqfYCgvOGbZse+f3NcCVz6kb+LIprEwPt8LN6NLPPvO6eMTurSB8hA==", "03fa27d1-6e20-443c-8ff8-945c12dc4ddd" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "63ddc161-5d8a-4cc5-a306-3529172c75cb");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "33e02e03-fb5a-4373-a494-09006ce07ecb");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "6717c3e7-1830-4218-98fd-a095e694cd4f");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ApprovedOn", "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { new DateTime(2023, 2, 2, 2, 23, 10, 795, DateTimeKind.Local).AddTicks(1140), "9cc85a07-58fe-4452-9534-6270f6015a08", new DateTime(2023, 2, 2, 2, 23, 10, 795, DateTimeKind.Local).AddTicks(1118), "AQAAAAEAACcQAAAAEKuRQCsjf4yzwU+x2qomvL/dVKp3OIKcwBl14oe8bgWmBBwzxRcRk0RGnQX/ueTSwA==", "c06a22d7-0357-47b1-9679-4cb10e9a96db" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ApprovedOn", "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { new DateTime(2023, 2, 2, 2, 23, 10, 795, DateTimeKind.Local).AddTicks(1154), "3a2237b0-f9d1-46e9-b03a-764d81e17116", new DateTime(2023, 2, 2, 2, 23, 10, 795, DateTimeKind.Local).AddTicks(1152), "AQAAAAEAACcQAAAAEGTta1FGzZlfcjek/IkSzjbXclvAbYFd2uTafQcfeUOW+02YWE2vkYtvqA9yZySGyA==", "7e345a0f-6986-4cbd-9db5-ce8639bbb4eb" });
        }
    }
}
