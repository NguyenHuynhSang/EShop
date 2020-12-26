using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Server.Migrations
{
    public partial class add_parent_comment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "ProductComment",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductComment_ParentId",
                table: "ProductComment",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductComment_ProductComment_ParentId",
                table: "ProductComment",
                column: "ParentId",
                principalTable: "ProductComment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductComment_ProductComment_ParentId",
                table: "ProductComment");

            migrationBuilder.DropIndex(
                name: "IX_ProductComment_ParentId",
                table: "ProductComment");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "ProductComment");
        }
    }
}
