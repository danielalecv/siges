using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace siges.Migrations {
  public partial class ContactoCliente : Migration {
    protected override void Up(MigrationBuilder migrationBuilder) {
      migrationBuilder.AddColumn<int>(
          name: "ContactoClienteId",
          table: "Persona",
          nullable: true);

      migrationBuilder.CreateTable(
          name: "ContactoCliente",
          columns: table => new {
          Id = table.Column<int>(nullable: false)
          .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
          Estatus = table.Column<bool>(nullable: false),
          ClienteId = table.Column<int>(nullable: true),
          CreadorPorId = table.Column<int>(nullable: true),
          ModificadoPorId = table.Column<int>(nullable: true),
          FechaCreacion = table.Column<DateTime>(nullable: false),
          FechaModificacion = table.Column<DateTime>(nullable: false)
          },
constraints: table => {
table.PrimaryKey("PK_ContactoCliente", x => x.Id);
table.ForeignKey(
    name: "FK_ContactoCliente_Cliente_ClienteId",
    column: x => x.ClienteId,
    principalTable: "Cliente",
    principalColumn: "Id",
    onDelete: ReferentialAction.Restrict);
table.ForeignKey(
    name: "FK_ContactoCliente_Persona_CreadorPorId",
    column: x => x.CreadorPorId,
    principalTable: "Persona",
    principalColumn: "Id",
    onDelete: ReferentialAction.Restrict);
table.ForeignKey(
    name: "FK_ContactoCliente_Persona_ModificadoPorId",
    column: x => x.ModificadoPorId,
    principalTable: "Persona",
    principalColumn: "Id",
    onDelete: ReferentialAction.Restrict);
});

migrationBuilder.CreateIndex(
    name: "IX_Persona_ContactoClienteId",
    table: "Persona",
    column: "ContactoClienteId");

migrationBuilder.CreateIndex(
    name: "IX_ContactoCliente_ClienteId",
    table: "ContactoCliente",
    column: "ClienteId");

migrationBuilder.CreateIndex(
    name: "IX_ContactoCliente_CreadorPorId",
    table: "ContactoCliente",
    column: "CreadorPorId");

migrationBuilder.CreateIndex(
    name: "IX_ContactoCliente_ModificadoPorId",
    table: "ContactoCliente",
    column: "ModificadoPorId");

migrationBuilder.AddForeignKey(
    name: "FK_Persona_ContactoCliente_ContactoClienteId",
    table: "Persona",
    column: "ContactoClienteId",
    principalTable: "ContactoCliente",
    principalColumn: "Id",
    onDelete: ReferentialAction.Restrict);
}

protected override void Down(MigrationBuilder migrationBuilder)
{
  migrationBuilder.DropForeignKey(
      name: "FK_Persona_ContactoCliente_ContactoClienteId",
      table: "Persona");

  migrationBuilder.DropTable(
      name: "ContactoCliente");

  migrationBuilder.DropIndex(
      name: "IX_Persona_ContactoClienteId",
      table: "Persona");

  migrationBuilder.DropColumn(
      name: "ContactoClienteId",
      table: "Persona");
}
}
}
