using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Server.Migrations
{
    public partial class add_seed_data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Attribute",
                columns: new[] { "ID", "Name" },
                values: new object[] { 1, "Kích cỡ" });

            migrationBuilder.InsertData(
                table: "AttributeValue",
                columns: new[] { "ID", "AttributeID", "Name" },
                values: new object[] { 1, 1, "Đỏ" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AttributeValue",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Attribute",
                keyColumn: "ID",
                keyValue: 1);
        }
    }
}
