using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Server.Migrations
{
    public partial class drop_old_seed_data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.InsertData(
            //    table: "Product",
            //    columns: new[] { "Id", "ApplyPromotion", "CatalogID", "Content", "CreatedBy", "CreatedDate", "Deliver", "Description", "ModifiedBy", "ModifiedDate", "Name", "OriginalPrice", "SEODescription", "SEOTitle", "SEOUrl", "Url", "Weight" },
            //    values: new object[] { 1, true, 1, "This is an iphone", null, null, true, "no discrip", null, null, "Iphone test", 16000000m, null, null, null, null, 0 });

            //migrationBuilder.InsertData(
            //    table: "Product",
            //    columns: new[] { "Id", "ApplyPromotion", "CatalogID", "Content", "CreatedBy", "CreatedDate", "Deliver", "Description", "ModifiedBy", "ModifiedDate", "Name", "OriginalPrice", "SEODescription", "SEOTitle", "SEOUrl", "Url", "Weight" },
            //    values: new object[] { 2, true, 1, "This is a samsung", null, null, true, "no discrip", null, null, "samsung galaxy test", 14000000m, null, null, null, null, 0 });

            //migrationBuilder.InsertData(
            //    table: "ProductVersions",
            //    columns: new[] { "Id", "Barcode", "Description", "Price", "ProductID", "PromotionPrice", "Quantity", "SKU", "WareHouseID" },
            //    values: new object[] { 1, "COC", "Màu đỏ dl 250", 19000000m, 1, 0m, 100, "Iphone test", 0 });

            //migrationBuilder.InsertData(
            //    table: "ProductVersions",
            //    columns: new[] { "Id", "Barcode", "Description", "Price", "ProductID", "PromotionPrice", "Quantity", "SKU", "WareHouseID" },
            //    values: new object[] { 2, "COC", "Màu xanh dl 250", 18000000m, 1, 0m, 100, "Iphone test", 0 });

            //migrationBuilder.InsertData(
            //    table: "ProductVersions",
            //    columns: new[] { "Id", "Barcode", "Description", "Price", "ProductID", "PromotionPrice", "Quantity", "SKU", "WareHouseID" },
            //    values: new object[] { 3, "COC", "Màu xanh dl 250", 16000000m, 2, 0m, 100, "Iphone test", 0 });
        }
    }
}
