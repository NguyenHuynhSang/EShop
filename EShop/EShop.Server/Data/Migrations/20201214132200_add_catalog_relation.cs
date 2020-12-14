using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Server.Migrations
{
    public partial class add_catalog_relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductCatalog_ParentID",
                table: "ProductCatalog",
                column: "ParentID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCatalog_ProductCatalog_ParentID",
                table: "ProductCatalog",
                column: "ParentID",
                principalTable: "ProductCatalog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCatalog_ProductCatalog_ParentID",
                table: "ProductCatalog");

            migrationBuilder.DropIndex(
                name: "IX_ProductCatalog_ParentID",
                table: "ProductCatalog");
        }
    }
}
