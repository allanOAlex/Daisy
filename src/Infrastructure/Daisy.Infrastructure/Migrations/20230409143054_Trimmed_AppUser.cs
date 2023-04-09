using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Daisy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TrimmedAppUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsAuthenticated",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RememberMe",
                table: "Users");

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
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8a1272c4-4617-4dd5-bd41-0fb27f234a6c", new DateTime(2023, 4, 9, 17, 30, 54, 296, DateTimeKind.Local).AddTicks(5635), "AQAAAAEAACcQAAAAECjk+h5WkxeYlrd6FyD0R7USH5ybUtcj3YdmxUvj8Phk1LKl8yfClfnTPkTLGVhQaw==", "55374b17-8e42-41ab-a62b-445b91407955" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ddf8da87-7d0b-4ce1-8401-d684f40faab1", new DateTime(2023, 4, 9, 17, 30, 54, 296, DateTimeKind.Local).AddTicks(5649), "AQAAAAEAACcQAAAAEKYZi7Ib6159gf0TbGJTBtd0oTInFhY9hKhNYFR7/6exLdQ7Um1edxYGge2fIa5QTg==", "a0044f06-9557-4161-b682-8fb0eb285140" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApprovedBy",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAuthenticated",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RememberMe",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "d5c7460c-c051-47b0-aabb-9e515e53ec22");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "bfaca219-0ca3-4551-9a4b-70b99ff8061c");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "6124b76f-b060-4b50-9f7a-0f0eea03979b");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ApprovedBy", "ApprovedOn", "ConcurrencyStamp", "CreatedOn", "IsActive", "IsAdmin", "IsApproved", "IsAuthenticated", "PasswordHash", "RememberMe", "SecurityStamp" },
                values: new object[] { 0, new DateTime(2023, 3, 2, 2, 13, 33, 186, DateTimeKind.Local).AddTicks(7096), "4a28389a-bcb9-4fa6-834d-bad64d229182", new DateTime(2023, 3, 2, 2, 13, 33, 186, DateTimeKind.Local).AddTicks(7083), true, false, true, false, "AQAAAAEAACcQAAAAEGD+qqEZ2GEaO0d8wkCrdMxMgb8VcLQr2aLgw8OOWP155rHhhnJOzMqHTedU3ANjKg==", false, "34265f9c-2889-4a92-a6b5-de38e1a5791d" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ApprovedBy", "ApprovedOn", "ConcurrencyStamp", "CreatedOn", "IsActive", "IsAdmin", "IsApproved", "IsAuthenticated", "PasswordHash", "RememberMe", "SecurityStamp" },
                values: new object[] { 0, new DateTime(2023, 3, 2, 2, 13, 33, 186, DateTimeKind.Local).AddTicks(7180), "422faa78-5bc7-4c42-8b93-004e5ce18acd", new DateTime(2023, 3, 2, 2, 13, 33, 186, DateTimeKind.Local).AddTicks(7179), true, false, true, false, "AQAAAAEAACcQAAAAENtClZlUSohlHSlLnjIDeZxEonlWpijuZ/Ld1bqmaZMURL1XQiMlGPyEEFm+8tYGpw==", false, "d5d2a81a-ace2-436e-a47f-38a180ff69fe" });
        }
    }
}
