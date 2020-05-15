using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Data.Migrations
{
    public partial class edit_foreignKey_format_nameID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WareHouse",
                table: "ProductVersions");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "ProductVersionImage");

            migrationBuilder.DropColumn(
                name: "productVersion",
                table: "ProductVersionImage");

            migrationBuilder.AddColumn<int>(
                name: "WareHouseID",
                table: "ProductVersions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ImageID",
                table: "ProductVersionImage",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductVersionID",
                table: "ProductVersionImage",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WareHouseID",
                table: "ProductVersions");

            migrationBuilder.DropColumn(
                name: "ImageID",
                table: "ProductVersionImage");

            migrationBuilder.DropColumn(
                name: "ProductVersionID",
                table: "ProductVersionImage");

            migrationBuilder.AddColumn<int>(
                name: "WareHouse",
                table: "ProductVersions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "ProductVersionImage",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "productVersion",
                table: "ProductVersionImage",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
