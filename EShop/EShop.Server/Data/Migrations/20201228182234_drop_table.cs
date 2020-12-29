using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Server.Migrations
{
    public partial class drop_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProductVersions");

            migrationBuilder.AddColumn<string>(
                name: "AddtitionalInformation",
                table: "Product",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddtitionalInformation",
                table: "Product");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProductVersions",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }
    }
}
