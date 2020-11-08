using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Server.Migrations
{
    public partial class add_table_seedlog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TagtName",
                table: "Tag");

            migrationBuilder.AddColumn<string>(
                name: "TagName",
                table: "Tag",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SeedLog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataVersion = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeedLog", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeedLog");

            migrationBuilder.DropColumn(
                name: "TagName",
                table: "Tag");

            migrationBuilder.AddColumn<string>(
                name: "TagtName",
                table: "Tag",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
