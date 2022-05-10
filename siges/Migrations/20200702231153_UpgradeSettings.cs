using Microsoft.EntityFrameworkCore.Migrations;

namespace siges.Migrations
{
    public partial class UpgradeSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "UsoDeKits",
                table: "Settings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UsoDePaquetes",
                table: "Settings",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsoDeKits",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "UsoDePaquetes",
                table: "Settings");
        }
    }
}
