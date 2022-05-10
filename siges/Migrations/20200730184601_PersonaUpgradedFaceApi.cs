using Microsoft.EntityFrameworkCore.Migrations;

namespace siges.Migrations
{
    public partial class PersonaUpgradedFaceApi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FaceApiMinCantEntrenamiento",
                table: "Settings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FaceApiCount",
                table: "Persona",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FaceApiId",
                table: "Persona",
                nullable: false,
                defaultValue: "SINFACEAPIID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FaceApiMinCantEntrenamiento",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "FaceApiCount",
                table: "Persona");

            migrationBuilder.DropColumn(
                name: "FaceApiId",
                table: "Persona");
        }
    }
}
