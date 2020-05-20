using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Data.Migrations
{
    public partial class seed_attribute_attribute_value : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Attribute",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "Màu sắc" },
                    { 2, "Dung lượng" }
                });

            migrationBuilder.InsertData(
                table: "AttributeValue",
                columns: new[] { "ID", "AttributeID", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Đỏ" },
                    { 2, 1, "Xanh" },
                    { 3, 1, "Tím" },
                    { 4, 2, "16gb" },
                    { 5, 2, "32gb" },
                    { 6, 2, "64gb" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Attribute",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Attribute",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AttributeValue",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AttributeValue",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AttributeValue",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AttributeValue",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AttributeValue",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AttributeValue",
                keyColumn: "ID",
                keyValue: 6);
        }
    }
}
