using Microsoft.EntityFrameworkCore.Migrations;

namespace Generic_POS_System.Migrations
{
    public partial class ChangedPoductType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "categoryId",
                table: "Product");

            migrationBuilder.AddColumn<string>(
                name: "productType",
                table: "Product",
                type: "char(15)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "productType",
                table: "Product");

            migrationBuilder.AddColumn<int>(
                name: "categoryId",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
