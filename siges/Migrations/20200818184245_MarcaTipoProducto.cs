using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace siges.Migrations
{
    public partial class MarcaTipoProducto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Marca",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(nullable: true),
                    Estatus = table.Column<bool>(nullable: false),
                    CreadorPorId = table.Column<string>(nullable: true),
                    ModificadoPorId = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    Opcional1 = table.Column<string>(nullable: true),
                    Opcional2 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marca", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Marca_AspNetUsers_CreadorPorId",
                        column: x => x.CreadorPorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Marca_AspNetUsers_ModificadoPorId",
                        column: x => x.ModificadoPorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TipoProducto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(nullable: true),
                    Estatus = table.Column<bool>(nullable: false),
                    CreadorPorId = table.Column<string>(nullable: true),
                    ModificadoPorId = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    Opcional1 = table.Column<string>(nullable: true),
                    Opcional2 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoProducto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TipoProducto_AspNetUsers_CreadorPorId",
                        column: x => x.CreadorPorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TipoProducto_AspNetUsers_ModificadoPorId",
                        column: x => x.ModificadoPorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Marca_CreadorPorId",
                table: "Marca",
                column: "CreadorPorId");

            migrationBuilder.CreateIndex(
                name: "IX_Marca_ModificadoPorId",
                table: "Marca",
                column: "ModificadoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoProducto_CreadorPorId",
                table: "TipoProducto",
                column: "CreadorPorId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoProducto_ModificadoPorId",
                table: "TipoProducto",
                column: "ModificadoPorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Marca");

            migrationBuilder.DropTable(
                name: "TipoProducto");
        }
    }
}
