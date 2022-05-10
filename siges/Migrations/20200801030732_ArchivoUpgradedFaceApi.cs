using Microsoft.EntityFrameworkCore.Migrations;

namespace siges.Migrations
{
    public partial class ArchivoUpgradedFaceApi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FaceApiFinalResponse",
                table: "Archivo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FaceApiFinalResponse",
                table: "Archivo");
        }
    }
}
