using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Server.Migrations
{
    public partial class fix_col_creactedDate_default_value : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Catalog",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 6, 12, 19, 58, 6, 781, DateTimeKind.Local).AddTicks(2946));

            migrationBuilder.UpdateData(
                table: "Catalog",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 6, 12, 19, 58, 6, 782, DateTimeKind.Local).AddTicks(3364));

            migrationBuilder.UpdateData(
                table: "Catalog",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 6, 12, 19, 58, 6, 782, DateTimeKind.Local).AddTicks(3431));

            migrationBuilder.UpdateData(
                table: "Catalog",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 6, 12, 19, 58, 6, 782, DateTimeKind.Local).AddTicks(3436));

            migrationBuilder.UpdateData(
                table: "Catalog",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2020, 6, 12, 19, 58, 6, 782, DateTimeKind.Local).AddTicks(3437));

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 6, 12, 19, 58, 6, 782, DateTimeKind.Local).AddTicks(4371));

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 6, 12, 19, 58, 6, 782, DateTimeKind.Local).AddTicks(8737));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Catalog",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 6, 12, 19, 55, 29, 422, DateTimeKind.Local).AddTicks(9995));

            migrationBuilder.UpdateData(
                table: "Catalog",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 6, 12, 19, 55, 29, 424, DateTimeKind.Local).AddTicks(388));

            migrationBuilder.UpdateData(
                table: "Catalog",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 6, 12, 19, 55, 29, 424, DateTimeKind.Local).AddTicks(449));

            migrationBuilder.UpdateData(
                table: "Catalog",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 6, 12, 19, 55, 29, 424, DateTimeKind.Local).AddTicks(453));

            migrationBuilder.UpdateData(
                table: "Catalog",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2020, 6, 12, 19, 55, 29, 424, DateTimeKind.Local).AddTicks(456));

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 6, 12, 19, 55, 29, 424, DateTimeKind.Local).AddTicks(1371));

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 6, 12, 19, 55, 29, 424, DateTimeKind.Local).AddTicks(5748));
        }
    }
}
