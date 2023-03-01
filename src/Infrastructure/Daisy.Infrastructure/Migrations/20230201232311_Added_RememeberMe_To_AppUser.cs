using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Daisy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedRememeberMeToAppUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "ApprovedOn", "ConcurrencyStamp", "CreatedOn", "PasswordHash", "RememberMe", "SecurityStamp" },
                values: new object[] { new DateTime(2023, 2, 2, 2, 23, 10, 795, DateTimeKind.Local).AddTicks(1140), "9cc85a07-58fe-4452-9534-6270f6015a08", new DateTime(2023, 2, 2, 2, 23, 10, 795, DateTimeKind.Local).AddTicks(1118), "AQAAAAEAACcQAAAAEKuRQCsjf4yzwU+x2qomvL/dVKp3OIKcwBl14oe8bgWmBBwzxRcRk0RGnQX/ueTSwA==", false, "c06a22d7-0357-47b1-9679-4cb10e9a96db" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ApprovedOn", "ConcurrencyStamp", "CreatedOn", "PasswordHash", "RememberMe", "SecurityStamp" },
                values: new object[] { new DateTime(2023, 2, 2, 2, 23, 10, 795, DateTimeKind.Local).AddTicks(1154), "3a2237b0-f9d1-46e9-b03a-764d81e17116", new DateTime(2023, 2, 2, 2, 23, 10, 795, DateTimeKind.Local).AddTicks(1152), "AQAAAAEAACcQAAAAEGTta1FGzZlfcjek/IkSzjbXclvAbYFd2uTafQcfeUOW+02YWE2vkYtvqA9yZySGyA==", false, "7e345a0f-6986-4cbd-9db5-ce8639bbb4eb" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RememberMe",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ba2bc109-f246-4ae8-8ce8-3df4ad4432ed");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "c77b476d-5963-46c3-a227-b49b174a3993");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "5ff46857-3e42-47b5-a10e-e42cb5344c63");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ApprovedOn", "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { new DateTime(2023, 1, 28, 6, 33, 33, 675, DateTimeKind.Local).AddTicks(6573), "36b093a2-fcbf-4f9a-a977-af58e26bb93d", new DateTime(2023, 1, 28, 6, 33, 33, 675, DateTimeKind.Local).AddTicks(6563), "AQAAAAEAACcQAAAAEEbbR3LYXjD2GsYuz4If8ZQtE9yqbYdWx94+VOEaZ0CtUbKBwjCuOmUmd5LdzhyxIw==", "9c11bd24-889e-4862-bbf0-26315abc5940" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ApprovedOn", "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { new DateTime(2023, 1, 28, 6, 33, 33, 675, DateTimeKind.Local).AddTicks(6579), "fb51f8dc-fd59-4253-840d-cf13040b135e", new DateTime(2023, 1, 28, 6, 33, 33, 675, DateTimeKind.Local).AddTicks(6578), "AQAAAAEAACcQAAAAEELuM/ACE63mfjKlX4GSvk+Wb3M+rKJLlOqwZUVliKPGq/nGICA7JiCkuqlIsVROXw==", "cf030fde-c38f-40f8-9b5b-a1bb4da732b5" });
        }
    }
}
