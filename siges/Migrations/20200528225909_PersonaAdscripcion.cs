using Microsoft.EntityFrameworkCore.Migrations;

namespace siges.Migrations
{
    public partial class PersonaAdscripcion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Adscripcion",
                table: "Persona",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adscripcion",
                table: "Persona");
        }
    }
}
