using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Server.Migrations
{
    public partial class change_table_ProductVersionImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageID",
                table: "ProductVersionImage");

            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "ProductVersionImage",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "ProductVersionImage",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "ProductVersionImage");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "ProductVersionImage");

            migrationBuilder.AddColumn<int>(
                name: "ImageID",
                table: "ProductVersionImage",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
