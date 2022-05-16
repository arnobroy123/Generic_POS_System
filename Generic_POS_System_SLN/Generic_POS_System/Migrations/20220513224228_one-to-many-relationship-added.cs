using Microsoft.EntityFrameworkCore.Migrations;

namespace Generic_POS_System.Migrations
{
    public partial class onetomanyrelationshipadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductArcade",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    URL = table.Column<string>(nullable: true),
                    ProductsproductId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductArcade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductArcade_Product_ProductsproductId",
                        column: x => x.ProductsproductId,
                        principalTable: "Product",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductArcade_ProductsproductId",
                table: "ProductArcade",
                column: "ProductsproductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductArcade");
        }
    }
}
