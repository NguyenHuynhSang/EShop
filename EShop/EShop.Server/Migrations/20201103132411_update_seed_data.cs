using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Server.Migrations
{
    public partial class update_seed_data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.UpdateData(
                table: "AttributeValue",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Nam");

            migrationBuilder.UpdateData(
                table: "AttributeValue",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Nữ");

            migrationBuilder.UpdateData(
                table: "AttributeValue",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AttributeID", "Name" },
                values: new object[] { 2, "S" });

            migrationBuilder.UpdateData(
                table: "AttributeValue",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "M");

            migrationBuilder.UpdateData(
                table: "AttributeValue",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "L");

            migrationBuilder.UpdateData(
                table: "AttributeValue",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "XL");

            migrationBuilder.UpdateData(
                table: "ProductCatalog",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Áo");

            migrationBuilder.UpdateData(
                table: "ProductCatalog",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Quần");

            migrationBuilder.UpdateData(
                table: "ProductCatalog",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Áo khoác");

            migrationBuilder.UpdateData(
                table: "ProductCatalog",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Áo thun");

            migrationBuilder.UpdateData(
                table: "ProductCatalog",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Name", "ParentID" },
                values: new object[] { "Áo sơ mi", 1 });

            migrationBuilder.InsertData(
                table: "ProductCatalog",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Image", "ModifiedBy", "ModifiedDate", "Name", "ParentID", "SEODescription", "SEOTitle", "SEOUrl" },
                values: new object[] { 6, null, null, null, null, null, "Áo khoác", 1, null, null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductCatalog",
                keyColumn: "Id",
                keyValue: 6);

         

            migrationBuilder.UpdateData(
                table: "AttributeValue",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Đỏ");

            migrationBuilder.UpdateData(
                table: "AttributeValue",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Xanh");

            migrationBuilder.UpdateData(
                table: "AttributeValue",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AttributeID", "Name" },
                values: new object[] { 1, "Tím" });

            migrationBuilder.UpdateData(
                table: "AttributeValue",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "16gb");

            migrationBuilder.UpdateData(
                table: "AttributeValue",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "32gb");

            migrationBuilder.UpdateData(
                table: "AttributeValue",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "64gb");

            migrationBuilder.UpdateData(
                table: "ProductCatalog",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Điện thoại");

            migrationBuilder.UpdateData(
                table: "ProductCatalog",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Laptop");

            migrationBuilder.UpdateData(
                table: "ProductCatalog",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Samsung");

            migrationBuilder.UpdateData(
                table: "ProductCatalog",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Apple");

            migrationBuilder.UpdateData(
                table: "ProductCatalog",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Name", "ParentID" },
                values: new object[] { "Macbook", 2 });
        }
    }
}
