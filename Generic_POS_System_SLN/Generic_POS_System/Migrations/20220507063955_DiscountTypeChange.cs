using Microsoft.EntityFrameworkCore.Migrations;

namespace Generic_POS_System.Migrations
{
    public partial class DiscountTypeChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "productDiscount",
                table: "Product",
                type: "decimal(7,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "productDiscount",
                table: "Product",
                type: "decimal(3,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldNullable: true);
        }
    }
}
