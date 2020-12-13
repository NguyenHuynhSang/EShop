using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Server.Migrations
{
    public partial class seed_menugroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MenuGroup",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "ADMIN" });

            migrationBuilder.InsertData(
                table: "MenuGroup",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "CLIENT" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuGroup",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MenuGroup",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
