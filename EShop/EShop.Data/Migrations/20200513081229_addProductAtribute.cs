using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Data.Migrations
{
    public partial class addProductAtribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "Product");

            migrationBuilder.AddColumn<bool>(
                name: "ApplyPromotion",
                table: "Product",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "CatalogID",
                table: "Product",
                maxLength: 500,
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Product",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deliver",
                table: "Product",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Product",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Product",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Product",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "OriginalPrice",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SEODescription",
                table: "Product",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SEOTitle",
                table: "Product",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SEOUrl",
                table: "Product",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Product",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "Product",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplyPromotion",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CatalogID",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Deliver",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "OriginalPrice",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "SEODescription",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "SEOTitle",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "SEOUrl",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Product");

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Product",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "UnitPrice",
                table: "Product",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
