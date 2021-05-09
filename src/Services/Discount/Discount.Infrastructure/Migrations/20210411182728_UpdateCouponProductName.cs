using Microsoft.EntityFrameworkCore.Migrations;

namespace Discount.Infrastructure.Migrations
{
    public partial class UpdateCouponProductName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                schema: "discount",
                table: "Coupons",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_ProductName",
                schema: "discount",
                table: "Coupons",
                column: "ProductName",
                unique: true,
                filter: "[ProductName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Coupons_ProductName",
                schema: "discount",
                table: "Coupons");

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                schema: "discount",
                table: "Coupons",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
