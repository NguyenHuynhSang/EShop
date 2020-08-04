using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Server.Migrations
{
    public partial class seed_productVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CatalogID", "OriginalPrice" },
                values: new object[] { 1L, 16000000m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "CatalogID", "OriginalPrice" },
                values: new object[] { 1L, 14000000m });

            migrationBuilder.InsertData(
                table: "ProductVersions",
                columns: new[] { "ID", "Barcode", "Description", "Price", "ProductID", "Quantum", "RemainingAmount", "SKU", "WareHouseID" },
                values: new object[,]
                {
                    { 1, "COC", "Màu đỏ dl 250", 19000000m, 1, 100, 100, "Iphone test", 0 },
                    { 2, "COC", "Màu xanh dl 250", 18000000m, 1, 100, 100, "Iphone test", 0 },
                    { 3, "COC", "Màu xanh dl 250", 16000000m, 2, 100, 100, "Iphone test", 0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductVersions",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductVersions",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductVersions",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CatalogID", "OriginalPrice" },
                values: new object[] { 0L, null });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "CatalogID", "OriginalPrice" },
                values: new object[] { 0L, null });
        }
    }
}
