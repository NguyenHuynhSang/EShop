using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Server.Migrations
{
    public partial class drop_navigator_property_use_foreignKey_instead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Product",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CatalogID",
                table: "Product",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldMaxLength: 500);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 1,
                column: "CatalogID",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 2,
                column: "CatalogID",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_ProductVersions_ProductID",
                table: "ProductVersions",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttribute_AttributeValueID",
                table: "ProductAttribute",
                column: "AttributeValueID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttribute_ProductVersionID",
                table: "ProductAttribute",
                column: "ProductVersionID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CatalogID",
                table: "Product",
                column: "CatalogID");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeValue_AttributeID",
                table: "AttributeValue",
                column: "AttributeID");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeValue_Attribute_AttributeID",
                table: "AttributeValue",
                column: "AttributeID",
                principalTable: "Attribute",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Catalog_CatalogID",
                table: "Product",
                column: "CatalogID",
                principalTable: "Catalog",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttribute_AttributeValue_AttributeValueID",
                table: "ProductAttribute",
                column: "AttributeValueID",
                principalTable: "AttributeValue",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttribute_ProductVersions_ProductVersionID",
                table: "ProductAttribute",
                column: "ProductVersionID",
                principalTable: "ProductVersions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVersions_Product_ProductID",
                table: "ProductVersions",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeValue_Attribute_AttributeID",
                table: "AttributeValue");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Catalog_CatalogID",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttribute_AttributeValue_AttributeValueID",
                table: "ProductAttribute");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttribute_ProductVersions_ProductVersionID",
                table: "ProductAttribute");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVersions_Product_ProductID",
                table: "ProductVersions");

            migrationBuilder.DropIndex(
                name: "IX_ProductVersions_ProductID",
                table: "ProductVersions");

            migrationBuilder.DropIndex(
                name: "IX_ProductAttribute_AttributeValueID",
                table: "ProductAttribute");

            migrationBuilder.DropIndex(
                name: "IX_ProductAttribute_ProductVersionID",
                table: "ProductAttribute");

            migrationBuilder.DropIndex(
                name: "IX_Product_CatalogID",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_AttributeValue_AttributeID",
                table: "AttributeValue");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Product",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CatalogID",
                table: "Product",
                type: "bigint",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(int),
                oldMaxLength: 500);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 1,
                column: "CatalogID",
                value: 1L);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 2,
                column: "CatalogID",
                value: 1L);
        }
    }
}
