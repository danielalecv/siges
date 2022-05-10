using Microsoft.EntityFrameworkCore.Migrations;

namespace siges.Migrations
{
    public partial class SettingsUpgradedFaceApi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FaceApiIterarRequestUntil90OrMore",
                table: "Settings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FaceApiMantenerHistorico",
                table: "Settings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FaceApiUso",
                table: "Settings",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FaceApiIterarRequestUntil90OrMore",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "FaceApiMantenerHistorico",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "FaceApiUso",
                table: "Settings");
        }
    }
}
