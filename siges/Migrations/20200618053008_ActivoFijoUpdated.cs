using Microsoft.EntityFrameworkCore.Migrations;

namespace siges.Migrations
{
    public partial class ActivoFijoUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NumeroSerie",
                table: "ActivoFijo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroSerie",
                table: "ActivoFijo");
        }
    }
}
