using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace siges.Migrations
{
    public partial class ClienteIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClienteIdentity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClienteId = table.Column<int>(nullable: true),
                    CuentaUsuarioId = table.Column<string>(nullable: true),
                    Estatus = table.Column<bool>(nullable: false),
                    FechaAdministrativa = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteIdentity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClienteIdentity_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClienteIdentity_AspNetUsers_CuentaUsuarioId",
                        column: x => x.CuentaUsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClienteIdentity_ClienteId",
                table: "ClienteIdentity",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteIdentity_CuentaUsuarioId",
                table: "ClienteIdentity",
                column: "CuentaUsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClienteIdentity");
        }
    }
}
