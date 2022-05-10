using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace siges.Migrations
{
    public partial class EstructuraExifBi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstructuraExifId",
                table: "Archivo",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EstructuraExifBi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateTimeOriginalId = table.Column<int>(nullable: false),
                    DateTimeOriginal = table.Column<DateTime>(nullable: false),
                    GPSLongitudeId = table.Column<int>(nullable: false),
                    GPSLongitudeValue = table.Column<string>(nullable: true),
                    GPSLongitudeDescription = table.Column<double>(nullable: false),
                    GPSLongitudeRefId = table.Column<int>(nullable: false),
                    GPSLongitudeRefValue = table.Column<string>(nullable: true),
                    GPSLongitudeRefDescription = table.Column<string>(nullable: true),
                    GPSLatitudeId = table.Column<int>(nullable: false),
                    GPSLatitudeValue = table.Column<string>(nullable: true),
                    GPSLatitudeDescription = table.Column<string>(nullable: true),
                    GPSLatitudeRefId = table.Column<int>(nullable: false),
                    GPSLatitudeRefValue = table.Column<string>(nullable: true),
                    GPSLatitudeRefDescription = table.Column<string>(nullable: true),
                    GPSAltitudeId = table.Column<int>(nullable: false),
                    GPSAltitudeValue = table.Column<string>(nullable: true),
                    GPSAltitudeDescription = table.Column<string>(nullable: true),
                    GPSAltitudeRefId = table.Column<int>(nullable: false),
                    GPSAltitudeRefValue = table.Column<int>(nullable: false),
                    GPSAltitudeRefDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstructuraExifBi", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Archivo_EstructuraExifId",
                table: "Archivo",
                column: "EstructuraExifId");

            migrationBuilder.AddForeignKey(
                name: "FK_Archivo_EstructuraExifBi_EstructuraExifId",
                table: "Archivo",
                column: "EstructuraExifId",
                principalTable: "EstructuraExifBi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Archivo_EstructuraExifBi_EstructuraExifId",
                table: "Archivo");

            migrationBuilder.DropTable(
                name: "EstructuraExifBi");

            migrationBuilder.DropIndex(
                name: "IX_Archivo_EstructuraExifId",
                table: "Archivo");

            migrationBuilder.DropColumn(
                name: "EstructuraExifId",
                table: "Archivo");
        }
    }
}
