using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Data.Migrations
{
    public partial class addCatalogTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Catalog",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentID = table.Column<int>(nullable: true),
                    SEOTitle = table.Column<string>(maxLength: 500, nullable: true),
                    SEOUrl = table.Column<string>(maxLength: 500, nullable: true),
                    SEODescription = table.Column<string>(maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalog", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Catalog");
        }
    }
}
