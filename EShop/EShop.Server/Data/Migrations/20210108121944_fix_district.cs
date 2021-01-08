using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Server.Migrations
{
    public partial class fix_district : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ward_District_DistrictId",
                table: "Ward");

            migrationBuilder.DropPrimaryKey(
                name: "PK_District",
                table: "District");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "District");

            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "District",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_District",
                table: "District",
                column: "DistrictId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ward_District_DistrictId",
                table: "Ward",
                column: "DistrictId",
                principalTable: "District",
                principalColumn: "DistrictId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ward_District_DistrictId",
                table: "Ward");

            migrationBuilder.DropPrimaryKey(
                name: "PK_District",
                table: "District");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "District");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "District",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_District",
                table: "District",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ward_District_DistrictId",
                table: "Ward",
                column: "DistrictId",
                principalTable: "District",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
