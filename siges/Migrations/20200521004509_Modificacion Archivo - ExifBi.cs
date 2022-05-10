using Microsoft.EntityFrameworkCore.Migrations;

namespace siges.Migrations
{
    public partial class ModificacionArchivoExifBi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExifBi",
                table: "Archivo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExifBi",
                table: "Archivo");
        }
    }
}
