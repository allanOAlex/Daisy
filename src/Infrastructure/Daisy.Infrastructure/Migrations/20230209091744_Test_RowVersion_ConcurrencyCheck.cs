using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Daisy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TestRowVersionConcurrencyCheck : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Users",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Users");

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
    }
}
