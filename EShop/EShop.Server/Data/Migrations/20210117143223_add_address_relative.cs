using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Server.Migrations
{
    public partial class add_address_relative : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "ShippingDetail",
                table: "Orders",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AddressToShips",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WardCode = table.Column<string>(nullable: true),
                    AddressDetail = table.Column<string>(nullable: true),
                    isMain = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressToShips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AddressToShips_Ward_WardCode",
                        column: x => x.WardCode,
                        principalTable: "Ward",
                        principalColumn: "WardCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddressToShips_WardCode",
                table: "AddressToShips",
                column: "WardCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddressToShips");

            migrationBuilder.DropColumn(
                name: "ShippingDetail",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
