using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Server.Migrations
{
    public partial class remove_seed_data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Attribute",
                keyColumn: "ID",
                keyValue: 1);

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

            migrationBuilder.DeleteData(
                table: "AttributeValue",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Attribute",
                keyColumn: "ID",
                keyValue: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Attribute",
                columns: new[] { "ID", "Name" },
                values: new object[] { 1, "Màu sắc" });

            migrationBuilder.InsertData(
                table: "Attribute",
                columns: new[] { "ID", "Name" },
                values: new object[] { 2, "Kích cỡ" });

            migrationBuilder.InsertData(
                table: "AttributeValue",
                columns: new[] { "ID", "AttributeID", "Name" },
                values: new object[,]
                {
                    { 3, 2, "S" },
                    { 4, 2, "M" },
                    { 5, 2, "L" },
                    { 6, 2, "XXL" },
                    { 7, 2, "XL" }
                });
        }
    }
}
