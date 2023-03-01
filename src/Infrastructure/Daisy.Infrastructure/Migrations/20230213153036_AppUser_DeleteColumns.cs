using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Daisy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AppUserDeleteColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
                columns: new[] { "ApprovedOn", "ConcurrencyStamp", "CreatedOn", "DeletedBy", "DeletedOn", "IsDeleted", "PasswordHash", "SecurityStamp" },
                values: new object[] { new DateTime(2023, 2, 13, 18, 30, 35, 986, DateTimeKind.Local).AddTicks(773), "61a09d4d-f1a5-45ea-b192-a5be25b34a1a", new DateTime(2023, 2, 13, 18, 30, 35, 986, DateTimeKind.Local).AddTicks(762), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "AQAAAAEAACcQAAAAEOsW/OdqyE2XOb4+q8C08+OiYYEeojk9lJEKfDe2UKIpVJXVU1SgBXqlgYUH4TLzyQ==", "eb226653-a6d2-45ab-ae4e-ad6904d9c18f" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ApprovedOn", "ConcurrencyStamp", "CreatedOn", "DeletedBy", "DeletedOn", "IsDeleted", "PasswordHash", "SecurityStamp" },
                values: new object[] { new DateTime(2023, 2, 13, 18, 30, 35, 986, DateTimeKind.Local).AddTicks(790), "72bd3004-3452-4145-946d-fe9937f043e8", new DateTime(2023, 2, 13, 18, 30, 35, 986, DateTimeKind.Local).AddTicks(789), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "AQAAAAEAACcQAAAAEPjG5FHoQBTnWNaCuT/4hMSiRK144dNt/viMNvTuX+pfhj+sz4SjpKNAC/fgA+tHgQ==", "bd0497f4-f38a-4521-a0c9-d830d5b376ba" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "92bfbc98-fc49-40fc-a25b-11c740fc73a4");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "8b021e7e-42a2-4c30-937d-eaa8d1456652");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "8a7ee93f-021b-47f5-b036-6bd76bf59a2e");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ApprovedOn", "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { new DateTime(2023, 2, 9, 12, 17, 44, 616, DateTimeKind.Local).AddTicks(889), "bb84f829-b83c-4b1f-9bcb-dc948220fe61", new DateTime(2023, 2, 9, 12, 17, 44, 616, DateTimeKind.Local).AddTicks(876), "AQAAAAEAACcQAAAAEATAZI2vIIdtqUuPEfEFIVBrg6I5n2DVKeh+/Fg+88uaHs8sJZeLcAmY4vr2gKsBog==", "00414e98-7a56-46b9-ab77-ea21fea999f7" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ApprovedOn", "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { new DateTime(2023, 2, 9, 12, 17, 44, 616, DateTimeKind.Local).AddTicks(896), "a1515792-606c-4678-bb07-3841660aa6eb", new DateTime(2023, 2, 9, 12, 17, 44, 616, DateTimeKind.Local).AddTicks(896), "AQAAAAEAACcQAAAAEHQYRdx669MQ9s64WJ5P4eJYbfn3NJPTQo1+VhdMQr+dsTLTGAVNHQDf/r5T7qmh4A==", "c0dbb34e-52d9-4565-ae7f-e323b051f193" });
        }
    }
}
