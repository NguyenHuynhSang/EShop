using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Server.Migrations
{
    public partial class add_foreignKey_table_productVersionImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductVersionImage_ProductVersionID",
                table: "ProductVersionImage",
                column: "ProductVersionID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVersionImage_ProductVersions_ProductVersionID",
                table: "ProductVersionImage",
                column: "ProductVersionID",
                principalTable: "ProductVersions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVersionImage_ProductVersions_ProductVersionID",
                table: "ProductVersionImage");

            migrationBuilder.DropIndex(
                name: "IX_ProductVersionImage_ProductVersionID",
                table: "ProductVersionImage");
        }
    }
}
