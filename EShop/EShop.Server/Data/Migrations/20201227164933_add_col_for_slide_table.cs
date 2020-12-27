using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Server.Migrations
{
    public partial class add_col_for_slide_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ButtonText",
                table: "Slides",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Slides",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ButtonText",
                table: "Slides");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Slides");
        }
    }
}
