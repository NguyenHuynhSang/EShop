using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Data.Migrations
{
    public partial class seed_catalog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Catalog",
                columns: new[] { "ID", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "Name", "ParentID", "SEODescription", "SEOTitle", "SEOUrl" },
                values: new object[,]
                {
                    { 1, null, null, null, null, "Điện thoại", null, null, null, null },
                    { 2, null, null, null, null, "Laptop", null, null, null, null },
                    { 3, null, null, null, null, "Samsung", 1, null, null, null },
                    { 4, null, null, null, null, "Apple", 1, null, null, null },
                    { 5, null, null, null, null, "Macbook", 2, null, null, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Catalog",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Catalog",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Catalog",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Catalog",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Catalog",
                keyColumn: "ID",
                keyValue: 5);
        }
    }
}
