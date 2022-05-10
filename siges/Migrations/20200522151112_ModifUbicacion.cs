using Microsoft.EntityFrameworkCore.Migrations;

namespace siges.Migrations
{
    public partial class ModifUbicacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "Ubicacion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                table: "Ubicacion",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Ubicacion");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Ubicacion");
        }
    }
}
