using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace siges.Migrations
{
    public partial class OsRecurrente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Persona",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "IndexListOSDTO",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Folio = table.Column<string>(nullable: true),
                    FechaInicio = table.Column<DateTime>(nullable: true),
                    Cliente = table.Column<string>(nullable: true),
                    Contrato = table.Column<string>(nullable: true),
                    ContratoTipo = table.Column<string>(nullable: true),
                    Ubicacion = table.Column<string>(nullable: true),
                    Servicio = table.Column<string>(nullable: true),
                    EstatusServicio = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndexListOSDTO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OsRecurrente",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OsOrigenId = table.Column<int>(nullable: false),
                    Periodo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OsRecurrente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Oses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OsId = table.Column<int>(nullable: false),
                    OsRecurrenteId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Oses_OsRecurrente_OsRecurrenteId",
                        column: x => x.OsRecurrenteId,
                        principalTable: "OsRecurrente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Oses_OsRecurrenteId",
                table: "Oses",
                column: "OsRecurrenteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IndexListOSDTO");

            migrationBuilder.DropTable(
                name: "Oses");

            migrationBuilder.DropTable(
                name: "OsRecurrente");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Persona");
        }
    }
}
