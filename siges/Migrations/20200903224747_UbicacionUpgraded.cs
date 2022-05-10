using Microsoft.EntityFrameworkCore.Migrations;

namespace siges.Migrations
{
    public partial class UbicacionUpgraded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Distancia",
                table: "Ubicacion",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Distancia",
                table: "Ubicacion");
        }
    }
}
