using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Server.Migrations
{
    public partial class up_order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShipEmail",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShipName",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShipPhone",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShipEmail",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShipName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShipPhone",
                table: "Orders");
        }
    }
}
