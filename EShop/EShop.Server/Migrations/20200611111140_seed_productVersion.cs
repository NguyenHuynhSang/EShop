using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Server.Migrations
{
    public partial class seed_productVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CatalogID", "OriginalPrice" },
                values: new object[] { 1L, 16000000m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CatalogID", "OriginalPrice" },
                values: new object[] { 1L, 14000000m });

            //migrationBuilder.InsertData(
            //    table: "ProductVersions",
            //    columns: new[] { "Id", "Barcode", "Description", "Price", "ProductID", "Quantity", "SKU" },
            //    values: new object[,]
            //    {
            //        { 1, "COC", "Màu đỏ dl 250", 19000000m, 1, 100, "Iphone test"},
            //        { 2, "COC", "Màu xanh dl 250", 18000000m, 1, 100, "Iphone test"},
            //        { 3, "COC", "Màu xanh dl 250", 16000000m, 2, 100, "Iphone test"}
            //    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductVersions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductVersions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductVersions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CatalogID", "OriginalPrice" },
                values: new object[] { 0L, null });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CatalogID", "OriginalPrice" },
                values: new object[] { 0L, null });
        }
    }
}
