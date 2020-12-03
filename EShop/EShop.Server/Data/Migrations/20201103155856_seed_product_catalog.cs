using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Server.Migrations
{
    public partial class seed_product_catalog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ProductCatalog",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Image", "ModifiedBy", "ModifiedDate", "Name", "ParentID", "SEODescription", "SEOTitle", "SEOUrl" },
                values: new object[,]
                {
                    { 1, null, null, null, null, null, "Áo", null, null, null, null },
                    { 2, null, null, null, null, null, "Quần", null, null, null, null },
                    { 3, null, null, null, null, null, "Váy", null, null, null, null },
                    { 4, null, null, null, null, null, "Áo thun", 1, null, null, null },
                    { 5, null, null, null, null, null, "Áo sơ mi", 1, null, null, null },
                    { 6, null, null, null, null, null, "Áo khoác", 1, null, null, null },
                    { 7, null, null, null, null, null, "Quần tây", 2, null, null, null },
                    { 8, null, null, null, null, null, "Quần jean", 2, null, null, null },
                    { 9, null, null, null, null, null, "Quần kari", 2, null, null, null },
                    { 10, null, null, null, null, null, "Quần short", 2, null, null, null },
                    { 11, null, null, null, null, null, "Váy toàn thân", 3, null, null, null },
                    { 12, null, null, null, null, null, "Váy quần", 3, null, null, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductCatalog",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductCatalog",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductCatalog",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductCatalog",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductCatalog",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductCatalog",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProductCatalog",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductCatalog",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProductCatalog",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ProductCatalog",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ProductCatalog",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ProductCatalog",
                keyColumn: "Id",
                keyValue: 12);
        }
    }
}
