using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace siges.Migrations
{
    public partial class MigracionHistoryInventario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Caducidad",
                table: "OrdenInsumo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lote",
                table: "OrdenInsumo",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Kit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(nullable: true),
                    CreaId = table.Column<int>(nullable: true),
                    Estatus = table.Column<bool>(nullable: false),
                    FechaAdmin = table.Column<DateTime>(nullable: false),
                    Observaciones = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kit_Persona_CreaId",
                        column: x => x.CreaId,
                        principalTable: "Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lote",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(nullable: true),
                    InsumoId = table.Column<int>(nullable: true),
                    Caducidad = table.Column<DateTime>(nullable: false),
                    PersonaId = table.Column<int>(nullable: true),
                    Estatus = table.Column<bool>(nullable: false),
                    FechaAlta = table.Column<DateTime>(nullable: false),
                    Cantidad = table.Column<int>(nullable: false),
                    Observaciones = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lote_Insumo_InsumoId",
                        column: x => x.InsumoId,
                        principalTable: "Insumo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lote_Persona_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Paquete",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Clasificacion = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    CreaId = table.Column<int>(nullable: true),
                    Estatus = table.Column<bool>(nullable: false),
                    FechaAdmin = table.Column<DateTime>(nullable: false),
                    Observaciones = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paquete", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Paquete_Persona_CreaId",
                        column: x => x.CreaId,
                        principalTable: "Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KitInsumo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InsumoId = table.Column<int>(nullable: true),
                    KitId = table.Column<int>(nullable: true),
                    Cantidad = table.Column<int>(nullable: false),
                    Estatus = table.Column<bool>(nullable: false),
                    FechaAdministrativa = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KitInsumo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KitInsumo_Insumo_InsumoId",
                        column: x => x.InsumoId,
                        principalTable: "Insumo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KitInsumo_Kit_KitId",
                        column: x => x.KitId,
                        principalTable: "Kit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaqueteInsumo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Estatus = table.Column<bool>(nullable: false),
                    InsumoId = table.Column<int>(nullable: true),
                    PaqueteId = table.Column<int>(nullable: true),
                    Cantidad = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaqueteInsumo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaqueteInsumo_Insumo_InsumoId",
                        column: x => x.InsumoId,
                        principalTable: "Insumo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaqueteInsumo_Paquete_PaqueteId",
                        column: x => x.PaqueteId,
                        principalTable: "Paquete",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kit_CreaId",
                table: "Kit",
                column: "CreaId");

            migrationBuilder.CreateIndex(
                name: "IX_KitInsumo_InsumoId",
                table: "KitInsumo",
                column: "InsumoId");

            migrationBuilder.CreateIndex(
                name: "IX_KitInsumo_KitId",
                table: "KitInsumo",
                column: "KitId");

            migrationBuilder.CreateIndex(
                name: "IX_Lote_InsumoId",
                table: "Lote",
                column: "InsumoId");

            migrationBuilder.CreateIndex(
                name: "IX_Lote_PersonaId",
                table: "Lote",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_Paquete_CreaId",
                table: "Paquete",
                column: "CreaId");

            migrationBuilder.CreateIndex(
                name: "IX_PaqueteInsumo_InsumoId",
                table: "PaqueteInsumo",
                column: "InsumoId");

            migrationBuilder.CreateIndex(
                name: "IX_PaqueteInsumo_PaqueteId",
                table: "PaqueteInsumo",
                column: "PaqueteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KitInsumo");

            migrationBuilder.DropTable(
                name: "Lote");

            migrationBuilder.DropTable(
                name: "PaqueteInsumo");

            migrationBuilder.DropTable(
                name: "Kit");

            migrationBuilder.DropTable(
                name: "Paquete");

            migrationBuilder.DropColumn(
                name: "Caducidad",
                table: "OrdenInsumo");

            migrationBuilder.DropColumn(
                name: "Lote",
                table: "OrdenInsumo");
        }
    }
}
