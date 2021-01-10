using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Server.Migrations
{
    public partial class fix_name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EnName",
                table: "Tag",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnName",
                table: "Tag");
        }
    }
}
