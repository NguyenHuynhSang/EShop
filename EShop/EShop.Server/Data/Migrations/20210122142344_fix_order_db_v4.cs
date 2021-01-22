using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Server.Migrations
{
    public partial class fix_order_db_v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isMain",
                table: "AddressToShips");

            migrationBuilder.AddColumn<int>(
                name: "AddressToShipId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AddressToShipId",
                table: "Orders",
                column: "AddressToShipId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AddressToShips_AddressToShipId",
                table: "Orders",
                column: "AddressToShipId",
                principalTable: "AddressToShips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AddressToShips_AddressToShipId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_AddressToShipId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AddressToShipId",
                table: "Orders");

            migrationBuilder.AddColumn<bool>(
                name: "isMain",
                table: "AddressToShips",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
