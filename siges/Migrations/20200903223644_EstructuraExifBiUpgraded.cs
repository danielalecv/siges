using Microsoft.EntityFrameworkCore.Migrations;

namespace siges.Migrations
{
    public partial class EstructuraExifBiUpgraded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Distancia",
                table: "EstructuraExifBi",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Distancia",
                table: "EstructuraExifBi");
        }
    }
}
