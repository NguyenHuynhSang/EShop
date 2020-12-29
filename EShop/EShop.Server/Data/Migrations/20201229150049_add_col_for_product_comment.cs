using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Server.Migrations
{
    public partial class add_col_for_product_comment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FromDay",
                table: "ProductComment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Helpful",
                table: "ProductComment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "ProductComment",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnHelpful",
                table: "ProductComment",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromDay",
                table: "ProductComment");

            migrationBuilder.DropColumn(
                name: "Helpful",
                table: "ProductComment");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "ProductComment");

            migrationBuilder.DropColumn(
                name: "UnHelpful",
                table: "ProductComment");
        }
    }
}
