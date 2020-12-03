using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Server.Migrations
{
    public partial class update_catalog_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Product");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ProductCatalog",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ProductCatalog");

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
