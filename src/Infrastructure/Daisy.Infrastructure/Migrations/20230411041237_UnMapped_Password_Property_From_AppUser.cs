using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Daisy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UnMappedPasswordPropertyFromAppUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "fc5f06c2-cb6b-474d-9882-5e7d85472bd3");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "90bfe0b4-4279-4ab4-a8f0-2ece59902421");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "6fe231e3-5c6a-43a0-8fc8-1ee1fca31732");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d9f393a6-dde3-4fcd-9424-711623fed1b3", new DateTime(2023, 4, 11, 7, 12, 37, 422, DateTimeKind.Local).AddTicks(7365), "AQAAAAEAACcQAAAAEDhsMfq/eaQeIMXTMyvoQcIp9dtTdfkZkIxlH2kISf0sT/NiW7LqEvi09ifk4GCr8g==", "6f3681d5-3cf8-4e5f-885a-8dbf135a1e4b" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "16cee0d3-12d8-416e-aa0c-80be5f1dd3b4", new DateTime(2023, 4, 11, 7, 12, 37, 422, DateTimeKind.Local).AddTicks(7380), "AQAAAAEAACcQAAAAEA1I5xrb1pZUu/mH0AK7mgOVoumDL5CvEeFZT3o1OVLmQ6yZifAE6vK2c8oh1cUoJQ==", "d6445927-66fa-4ed7-a2ec-033737371ae4" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "04fc2b48-11ca-4c57-9c58-d61ceb6c1522");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "865568f2-6724-4ae8-bbc3-5d6a0300f75c");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "3eab2015-6481-4d24-b21c-e8cdba9ec816");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "Password", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8a1272c4-4617-4dd5-bd41-0fb27f234a6c", new DateTime(2023, 4, 9, 17, 30, 54, 296, DateTimeKind.Local).AddTicks(5635), "PA$5@AUTH", "AQAAAAEAACcQAAAAECjk+h5WkxeYlrd6FyD0R7USH5ybUtcj3YdmxUvj8Phk1LKl8yfClfnTPkTLGVhQaw==", "55374b17-8e42-41ab-a62b-445b91407955" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "Password", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ddf8da87-7d0b-4ce1-8401-d684f40faab1", new DateTime(2023, 4, 9, 17, 30, 54, 296, DateTimeKind.Local).AddTicks(5649), "PA$5@AUTH", "AQAAAAEAACcQAAAAEKYZi7Ib6159gf0TbGJTBtd0oTInFhY9hKhNYFR7/6exLdQ7Um1edxYGge2fIa5QTg==", "a0044f06-9557-4161-b682-8fb0eb285140" });
        }
    }
}
