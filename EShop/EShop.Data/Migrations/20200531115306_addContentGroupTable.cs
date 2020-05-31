using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Data.Migrations
{
    public partial class addContentGroupTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContentCategories",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 250, nullable: true),
                    ParentID = table.Column<int>(nullable: false),
                    MetaTitle = table.Column<string>(maxLength: 250, nullable: true),
                    SeoTitle = table.Column<string>(maxLength: 250, nullable: true),
                    DisplayOrder = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreateBy = table.Column<string>(maxLength: 250, nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(maxLength: 250, nullable: true),
                    MetaKeywords = table.Column<string>(maxLength: 250, nullable: true),
                    MetaDescriptions = table.Column<string>(maxLength: 250, nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    ShowOnHome = table.Column<bool>(nullable: false),
                    Language = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentCategories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Contents",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 250, nullable: true),
                    MetaTitle = table.Column<string>(maxLength: 250, nullable: true),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    Image = table.Column<string>(maxLength: 250, nullable: true),
                    CategoryID = table.Column<int>(nullable: false),
                    Detail = table.Column<string>(type: "ntext", nullable: true),
                    Warranty = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreateBy = table.Column<string>(maxLength: 250, nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(maxLength: 250, nullable: true),
                    MetaKeywords = table.Column<string>(maxLength: 250, nullable: true),
                    MetaDescription = table.Column<string>(maxLength: 250, nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    TopHot = table.Column<DateTime>(nullable: false),
                    ViewCount = table.Column<int>(nullable: false),
                    Tags = table.Column<string>(maxLength: 250, nullable: true),
                    Language = table.Column<string>(maxLength: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contents", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ContentTags",
                columns: table => new
                {
                    ContentID = table.Column<int>(nullable: false),
                    TagID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentTags", x => new { x.TagID, x.ContentID });
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagID = table.Column<string>(maxLength: 50, nullable: false),
                    TagName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContentCategories");

            migrationBuilder.DropTable(
                name: "Contents");

            migrationBuilder.DropTable(
                name: "ContentTags");

            migrationBuilder.DropTable(
                name: "Tags");
        }
    }
}
