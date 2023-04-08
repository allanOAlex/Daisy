using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Daisy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ColumnPasswordResetTokenAppUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordResetToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

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
                columns: new[] { "ApprovedOn", "ConcurrencyStamp", "CreatedOn", "PasswordHash", "PasswordResetToken", "SecurityStamp" },
                values: new object[] { new DateTime(2023, 3, 2, 2, 13, 33, 186, DateTimeKind.Local).AddTicks(7096), "4a28389a-bcb9-4fa6-834d-bad64d229182", new DateTime(2023, 3, 2, 2, 13, 33, 186, DateTimeKind.Local).AddTicks(7083), "AQAAAAEAACcQAAAAEGD+qqEZ2GEaO0d8wkCrdMxMgb8VcLQr2aLgw8OOWP155rHhhnJOzMqHTedU3ANjKg==", null, "34265f9c-2889-4a92-a6b5-de38e1a5791d" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ApprovedOn", "ConcurrencyStamp", "CreatedOn", "PasswordHash", "PasswordResetToken", "SecurityStamp" },
                values: new object[] { new DateTime(2023, 3, 2, 2, 13, 33, 186, DateTimeKind.Local).AddTicks(7180), "422faa78-5bc7-4c42-8b93-004e5ce18acd", new DateTime(2023, 3, 2, 2, 13, 33, 186, DateTimeKind.Local).AddTicks(7179), "AQAAAAEAACcQAAAAENtClZlUSohlHSlLnjIDeZxEonlWpijuZ/Ld1bqmaZMURL1XQiMlGPyEEFm+8tYGpw==", null, "d5d2a81a-ace2-436e-a47f-38a180ff69fe" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordResetToken",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "2b69c33b-a209-4a1c-99a9-f0e32ce4baa5");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "c20a17d0-489f-4dc3-a3b1-2f4368a2495b");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "b06f3888-8eb4-4764-a782-0462d51173d0");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ApprovedOn", "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { new DateTime(2023, 2, 28, 7, 36, 46, 659, DateTimeKind.Local).AddTicks(6215), "0c183ece-6ce7-4d59-bc07-493e5b806433", new DateTime(2023, 2, 28, 7, 36, 46, 659, DateTimeKind.Local).AddTicks(6205), "AQAAAAEAACcQAAAAEHzLr9CM2ik0fBUPvV66WEd4pNkxPTt/JhmIVYKQcl/DVxwMyjKoEi07mICBFXsuxQ==", "3281c2bc-ce94-4224-9f5c-014bb8d299ae" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ApprovedOn", "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { new DateTime(2023, 2, 28, 7, 36, 46, 659, DateTimeKind.Local).AddTicks(6236), "b289c6a4-fe4d-4be5-8c9e-0b9f8b225a9a", new DateTime(2023, 2, 28, 7, 36, 46, 659, DateTimeKind.Local).AddTicks(6235), "AQAAAAEAACcQAAAAEC586x2w1MmmArPoN/wrvAkKX3cXGoGEav4ERvOM2Md5nGb4SB7YHJb2CX8dt9c0ZQ==", "c7695e5f-f995-4f6d-9732-72cdcfd2b36e" });
        }
    }
}
