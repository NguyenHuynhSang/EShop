using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Server.Migrations
{
    public partial class add_fix_v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductVersionTags",
                columns: table => new
                {
                    TagId = table.Column<int>(nullable: false),
                    ProductVersionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVersionTags", x => new { x.TagId, x.ProductVersionID });
                    table.ForeignKey(
                        name: "FK_ProductVersionTags_ProductVersions_ProductVersionID",
                        column: x => x.ProductVersionID,
                        principalTable: "ProductVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductVersionTags_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductVersionTags_ProductVersionID",
                table: "ProductVersionTags",
                column: "ProductVersionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductVersionTags");
        }
    }
}
