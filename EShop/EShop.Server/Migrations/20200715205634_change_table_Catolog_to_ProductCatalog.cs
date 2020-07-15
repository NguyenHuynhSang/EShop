using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Server.Migrations
{
    public partial class change_table_Catolog_to_ProductCatalog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Catalog_CatalogID",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Catalog",
                table: "Catalog");

            migrationBuilder.RenameTable(
                name: "Catalog",
                newName: "ProductCatalog");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCatalog",
                table: "ProductCatalog",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductCatalog_CatalogID",
                table: "Product",
                column: "CatalogID",
                principalTable: "ProductCatalog",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductCatalog_CatalogID",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCatalog",
                table: "ProductCatalog");

            migrationBuilder.RenameTable(
                name: "ProductCatalog",
                newName: "Catalog");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Catalog",
                table: "Catalog",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Catalog_CatalogID",
                table: "Product",
                column: "CatalogID",
                principalTable: "Catalog",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
