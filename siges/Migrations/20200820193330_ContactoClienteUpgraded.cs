using Microsoft.EntityFrameworkCore.Migrations;

namespace siges.Migrations
{
  public partial class ContactoClienteUpgraded : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropIndex(
          name: "IX_ContactoCliente_ModificadoPorId",
          table: "ContactoCliente");

      migrationBuilder.DropIndex(
          name: "IX_ContactoCliente_CreadorPorId",
          table: "ContactoCliente");

      migrationBuilder.DropForeignKey(
          name: "FK_ContactoCliente_Persona_CreadorPorId",
          table: "ContactoCliente");

      migrationBuilder.DropForeignKey(
          name: "FK_ContactoCliente_Persona_ModificadoPorId",
          table: "ContactoCliente");

      migrationBuilder.AlterColumn<string>(
          name: "ModificadoPorId",
          table: "ContactoCliente",
          nullable: true,
          oldClrType: typeof(int),
          oldNullable: true);

      migrationBuilder.AlterColumn<string>(
          name: "CreadorPorId",
          table: "ContactoCliente",
          nullable: true,
          oldClrType: typeof(int),
          oldNullable: true);

      migrationBuilder.AddColumn<string>(
          name: "Opcional1",
          table: "ContactoCliente",
          nullable: true);

      migrationBuilder.AddColumn<string>(
          name: "Opcional2",
          table: "ContactoCliente",
          nullable: true);

      migrationBuilder.AddForeignKey(
          name: "FK_ContactoCliente_AspNetUsers_CreadorPorId",
          table: "ContactoCliente",
          column: "CreadorPorId",
          principalTable: "AspNetUsers",
          principalColumn: "Id",
          onDelete: ReferentialAction.Restrict);

      migrationBuilder.AddForeignKey(
          name: "FK_ContactoCliente_AspNetUsers_ModificadoPorId",
          table: "ContactoCliente",
          column: "ModificadoPorId",
          principalTable: "AspNetUsers",
          principalColumn: "Id",
          onDelete: ReferentialAction.Restrict);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
          name: "FK_ContactoCliente_AspNetUsers_CreadorPorId",
          table: "ContactoCliente");

      migrationBuilder.DropForeignKey(
          name: "FK_ContactoCliente_AspNetUsers_ModificadoPorId",
          table: "ContactoCliente");

      migrationBuilder.DropColumn(
          name: "Opcional1",
          table: "ContactoCliente");

      migrationBuilder.DropColumn(
          name: "Opcional2",
          table: "ContactoCliente");

      migrationBuilder.AlterColumn<int>(
          name: "ModificadoPorId",
          table: "ContactoCliente",
          nullable: true,
          oldClrType: typeof(string),
          oldNullable: true);

      migrationBuilder.AlterColumn<int>(
          name: "CreadorPorId",
          table: "ContactoCliente",
          nullable: true,
          oldClrType: typeof(string),
          oldNullable: true);

      migrationBuilder.AddForeignKey(
          name: "FK_ContactoCliente_Persona_CreadorPorId",
          table: "ContactoCliente",
          column: "CreadorPorId",
          principalTable: "Persona",
          principalColumn: "Id",
          onDelete: ReferentialAction.Restrict);

      migrationBuilder.AddForeignKey(
          name: "FK_ContactoCliente_Persona_ModificadoPorId",
          table: "ContactoCliente",
          column: "ModificadoPorId",
          principalTable: "Persona",
          principalColumn: "Id",
          onDelete: ReferentialAction.Restrict);
    }
  }
}
