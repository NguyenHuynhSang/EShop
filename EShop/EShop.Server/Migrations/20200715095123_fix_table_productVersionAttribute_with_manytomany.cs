using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Server.Migrations
{
    public partial class fix_table_productVersionAttribute_with_manytomany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductAttribute");

            migrationBuilder.CreateTable(
                name: "ProductVersionAttributes",
                columns: table => new
                {
                    AttributeValueID = table.Column<int>(nullable: false),
                    ProductVersionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVersionAttributes", x => new { x.AttributeValueID, x.ProductVersionID });
                    table.ForeignKey(
                        name: "FK_ProductVersionAttributes_AttributeValue_AttributeValueID",
                        column: x => x.AttributeValueID,
                        principalTable: "AttributeValue",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductVersionAttributes_ProductVersions_ProductVersionID",
                        column: x => x.ProductVersionID,
                        principalTable: "ProductVersions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductVersionAttributes_ProductVersionID",
                table: "ProductVersionAttributes",
                column: "ProductVersionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductVersionAttributes");

            migrationBuilder.CreateTable(
                name: "ProductAttribute",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttributeValueID = table.Column<int>(type: "int", nullable: false),
                    ProductVersionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttribute", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductAttribute_AttributeValue_AttributeValueID",
                        column: x => x.AttributeValueID,
                        principalTable: "AttributeValue",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductAttribute_ProductVersions_ProductVersionID",
                        column: x => x.ProductVersionID,
                        principalTable: "ProductVersions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttribute_AttributeValueID",
                table: "ProductAttribute",
                column: "AttributeValueID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttribute_ProductVersionID",
                table: "ProductAttribute",
                column: "ProductVersionID");
        }
    }
}
