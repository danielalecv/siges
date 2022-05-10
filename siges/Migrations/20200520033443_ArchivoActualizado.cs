using Microsoft.EntityFrameworkCore.Migrations;

namespace siges.Migrations
{
    public partial class ArchivoActualizado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicio_LineaNegocio_LineaNegocioId",
                table: "Servicio");

            migrationBuilder.AlterColumn<int>(
                name: "LineaNegocioId",
                table: "Servicio",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Exif",
                table: "Archivo",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicio_LineaNegocio_LineaNegocioId",
                table: "Servicio",
                column: "LineaNegocioId",
                principalTable: "LineaNegocio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicio_LineaNegocio_LineaNegocioId",
                table: "Servicio");

            migrationBuilder.DropColumn(
                name: "Exif",
                table: "Archivo");

            migrationBuilder.AlterColumn<int>(
                name: "LineaNegocioId",
                table: "Servicio",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Servicio_LineaNegocio_LineaNegocioId",
                table: "Servicio",
                column: "LineaNegocioId",
                principalTable: "LineaNegocio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
