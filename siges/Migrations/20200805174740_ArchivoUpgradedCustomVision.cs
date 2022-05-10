using Microsoft.EntityFrameworkCore.Migrations;

namespace siges.Migrations
{
    public partial class ArchivoUpgradedCustomVision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomVisionResult",
                table: "Archivo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomVisionResult",
                table: "Archivo");
        }
    }
}
