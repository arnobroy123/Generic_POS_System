using Microsoft.EntityFrameworkCore.Migrations;

namespace Generic_POS_System.Migrations
{
    public partial class addedmanytomany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "catId",
                table: "Product",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    catId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.catId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_catId",
                table: "Product",
                column: "catId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_catId",
                table: "Product",
                column: "catId",
                principalTable: "Category",
                principalColumn: "catId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_catId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Product_catId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "catId",
                table: "Product");
        }
    }
}
