using Microsoft.EntityFrameworkCore.Migrations;

namespace siges.Migrations
{
    public partial class SettingsUpgradedCustomVision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CustomVisionUso",
                table: "Settings",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomVisionUso",
                table: "Settings");
        }
    }
}
