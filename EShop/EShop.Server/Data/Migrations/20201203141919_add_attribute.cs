using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Server.Migrations
{
    public partial class add_attribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Attribute",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Chất liệu vải" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Attribute",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
