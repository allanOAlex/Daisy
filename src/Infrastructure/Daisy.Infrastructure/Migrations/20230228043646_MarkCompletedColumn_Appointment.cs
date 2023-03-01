using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Daisy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MarkCompletedColumnAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompletedBy",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedOn",
                table: "Appointments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CompletedRemarks",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsComplete",
                table: "Appointments",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedBy",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "CompletedOn",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "CompletedRemarks",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "IsComplete",
                table: "Appointments");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "4b4283b7-b066-46c2-be65-76533bf389af");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "427f1b98-7e8f-46ba-8a3c-1f862673789b");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "02206939-fb9e-4722-955c-f6da5ace6140");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ApprovedOn", "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { new DateTime(2023, 2, 13, 18, 30, 35, 986, DateTimeKind.Local).AddTicks(773), "61a09d4d-f1a5-45ea-b192-a5be25b34a1a", new DateTime(2023, 2, 13, 18, 30, 35, 986, DateTimeKind.Local).AddTicks(762), "AQAAAAEAACcQAAAAEOsW/OdqyE2XOb4+q8C08+OiYYEeojk9lJEKfDe2UKIpVJXVU1SgBXqlgYUH4TLzyQ==", "eb226653-a6d2-45ab-ae4e-ad6904d9c18f" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ApprovedOn", "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { new DateTime(2023, 2, 13, 18, 30, 35, 986, DateTimeKind.Local).AddTicks(790), "72bd3004-3452-4145-946d-fe9937f043e8", new DateTime(2023, 2, 13, 18, 30, 35, 986, DateTimeKind.Local).AddTicks(789), "AQAAAAEAACcQAAAAEPjG5FHoQBTnWNaCuT/4hMSiRK144dNt/viMNvTuX+pfhj+sz4SjpKNAC/fgA+tHgQ==", "bd0497f4-f38a-4521-a0c9-d830d5b376ba" });
        }
    }
}
