using Microsoft.EntityFrameworkCore.Migrations;

namespace siges.Migrations
{
    public partial class InsumoModelUpgrated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LoteDId",
                table: "OrdenInsumo",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrdenInsumo_LoteDId",
                table: "OrdenInsumo",
                column: "LoteDId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenInsumo_Lote_LoteDId",
                table: "OrdenInsumo",
                column: "LoteDId",
                principalTable: "Lote",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdenInsumo_Lote_LoteDId",
                table: "OrdenInsumo");

            migrationBuilder.DropIndex(
                name: "IX_OrdenInsumo_LoteDId",
                table: "OrdenInsumo");

            migrationBuilder.DropColumn(
                name: "LoteDId",
                table: "OrdenInsumo");
        }
    }
}
