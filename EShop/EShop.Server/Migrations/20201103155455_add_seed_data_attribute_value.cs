using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Server.Migrations
{
    public partial class add_seed_data_attribute_value : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AttributeValue",
                columns: new[] { "ID", "AttributeID", "Name" },
                values: new object[,]
                {
                    { 10, 1, "S" },
                    { 11, 1, "M" },
                    { 12, 1, "L" },
                    { 13, 1, "XL" },
                    { 14, 1, "XXL" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AttributeValue",
                keyColumn: "ID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AttributeValue",
                keyColumn: "ID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AttributeValue",
                keyColumn: "ID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AttributeValue",
                keyColumn: "ID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "AttributeValue",
                keyColumn: "ID",
                keyValue: 14);
        }
    }
}
