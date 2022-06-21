using Microsoft.EntityFrameworkCore.Migrations;

namespace Generic_POS_System.Migrations
{
    public partial class nameRemovedFromOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
