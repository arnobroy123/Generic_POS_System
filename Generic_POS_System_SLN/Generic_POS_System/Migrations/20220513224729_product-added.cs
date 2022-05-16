using Microsoft.EntityFrameworkCore.Migrations;

namespace Generic_POS_System.Migrations
{
    public partial class productadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductArcade_Product_ProductsproductId",
                table: "ProductArcade");

            migrationBuilder.DropIndex(
                name: "IX_ProductArcade_ProductsproductId",
                table: "ProductArcade");

            migrationBuilder.DropColumn(
                name: "ProductsproductId",
                table: "ProductArcade");

            migrationBuilder.CreateIndex(
                name: "IX_ProductArcade_productId",
                table: "ProductArcade",
                column: "productId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductArcade_Product_productId",
                table: "ProductArcade",
                column: "productId",
                principalTable: "Product",
                principalColumn: "productId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductArcade_Product_productId",
                table: "ProductArcade");

            migrationBuilder.DropIndex(
                name: "IX_ProductArcade_productId",
                table: "ProductArcade");

            migrationBuilder.AddColumn<int>(
                name: "ProductsproductId",
                table: "ProductArcade",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductArcade_ProductsproductId",
                table: "ProductArcade",
                column: "ProductsproductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductArcade_Product_ProductsproductId",
                table: "ProductArcade",
                column: "ProductsproductId",
                principalTable: "Product",
                principalColumn: "productId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
