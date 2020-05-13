using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Data.Migrations
{
    public partial class add_Table_AttributeValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AttributeValue",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttributeID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeValue", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttributeValue");
        }
    }
}
