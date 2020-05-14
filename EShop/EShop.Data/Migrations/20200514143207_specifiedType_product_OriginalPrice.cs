using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Data.Migrations
{
    public partial class specifiedType_product_OriginalPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "OriginalPrice",
                table: "Product",
                type: "decimal(18,0)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ID", "ApplyPromotion", "CatalogID", "Content", "CreatedBy", "CreatedDate", "Deliver", "Description", "ModifiedBy", "ModifiedDate", "Name", "OriginalPrice", "SEODescription", "SEOTitle", "SEOUrl", "Url", "Weight" },
                values: new object[] { 1, true, 0L, "This is an iphone", null, null, true, "no discrip", null, null, "Iphone test", null, null, null, null, null, 0 });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ID", "ApplyPromotion", "CatalogID", "Content", "CreatedBy", "CreatedDate", "Deliver", "Description", "ModifiedBy", "ModifiedDate", "Name", "OriginalPrice", "SEODescription", "SEOTitle", "SEOUrl", "Url", "Weight" },
                values: new object[] { 2, true, 0L, "This is a samsung", null, null, true, "no discrip", null, null, "samsung galaxy test", null, null, null, null, null, 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "OriginalPrice",
                table: "Product",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)",
                oldNullable: true);
        }
    }
}
