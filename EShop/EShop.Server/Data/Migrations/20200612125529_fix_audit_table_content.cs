using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Server.Migrations
{
    public partial class fix_audit_table_content : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SeoTitle",
                table: "ContentCategory",
                newName: "SEOTitle");

            migrationBuilder.AlterColumn<string>(
                name: "SEOTitle",
                table: "ContentCategory",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ContentCategory",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "SEODescription",
                table: "ContentCategory",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SEOUrl",
                table: "ContentCategory",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Content",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Content",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "SEODescription",
                table: "Content",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SEOTitle",
                table: "Content",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SEOUrl",
                table: "Content",
                maxLength: 500,
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SEODescription",
                table: "ContentCategory");

            migrationBuilder.DropColumn(
                name: "SEOUrl",
                table: "ContentCategory");

            migrationBuilder.DropColumn(
                name: "SEODescription",
                table: "Content");

            migrationBuilder.DropColumn(
                name: "SEOTitle",
                table: "Content");

            migrationBuilder.DropColumn(
                name: "SEOUrl",
                table: "Content");

            migrationBuilder.RenameColumn(
                name: "SEOTitle",
                table: "ContentCategory",
                newName: "SeoTitle");

            migrationBuilder.AlterColumn<string>(
                name: "SeoTitle",
                table: "ContentCategory",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ContentCategory",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Content",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Content",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Catalog",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "Catalog",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "Catalog",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "Catalog",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "Catalog",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: null);
        }
    }
}
