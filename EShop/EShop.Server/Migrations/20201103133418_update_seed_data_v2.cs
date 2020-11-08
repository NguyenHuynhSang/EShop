using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Server.Migrations
{
    public partial class update_seed_data_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AttributeValue",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AttributeValue",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "AttributeValue",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "XXL");

            migrationBuilder.InsertData(
                table: "AttributeValue",
                columns: new[] { "Id", "AttributeID", "Name" },
                values: new object[] { 7, 2, "XL" });

            migrationBuilder.UpdateData(
                table: "ProductCatalog",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Name", "ParentID" },
                values: new object[] { "Váy", null });

            migrationBuilder.InsertData(
                table: "ProductCatalog",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Image", "ModifiedBy", "ModifiedDate", "Name", "ParentID", "SEODescription", "SEOTitle", "SEOUrl" },
                values: new object[,]
                {
                    { 7, null, null, null, null, null, "Áo khoác", 1, null, null, null },
                    { 8, null, null, null, null, null, "Quần tây", 2, null, null, null },
                    { 9, null, null, null, null, null, "Quần jean", 2, null, null, null },
                    { 10, null, null, null, null, null, "Quần kari", 2, null, null, null },
                    { 11, null, null, null, null, null, "Quần short", 2, null, null, null },
                    { 12, null, null, null, null, null, "Váy toàn thân", 3, null, null, null },
                    { 13, null, null, null, null, null, "Váy quần", 3, null, null, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AttributeValue",
                keyColumn: "Id",
                keyValue: 7);

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

            migrationBuilder.DeleteData(
                table: "ProductCatalog",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.UpdateData(
                table: "AttributeValue",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "XL");

            migrationBuilder.InsertData(
                table: "AttributeValue",
                columns: new[] { "Id", "AttributeID", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Nam" },
                    { 2, 1, "Nữ" }
                });

            migrationBuilder.UpdateData(
                table: "ProductCatalog",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Name", "ParentID" },
                values: new object[] { "Áo khoác", 1 });
        }
    }
}
