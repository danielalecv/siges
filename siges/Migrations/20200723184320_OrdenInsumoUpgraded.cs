using Microsoft.EntityFrameworkCore.Migrations;

namespace siges.Migrations
{
    public partial class OrdenInsumoUpgraded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LoteTypeId",
                table: "OrdenInsumo",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrdenInsumo_LoteTypeId",
                table: "OrdenInsumo",
                column: "LoteTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenInsumo_Lote_LoteTypeId",
                table: "OrdenInsumo",
                column: "LoteTypeId",
                principalTable: "Lote",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdenInsumo_Lote_LoteTypeId",
                table: "OrdenInsumo");

            migrationBuilder.DropIndex(
                name: "IX_OrdenInsumo_LoteTypeId",
                table: "OrdenInsumo");

            migrationBuilder.DropColumn(
                name: "LoteTypeId",
                table: "OrdenInsumo");
        }
    }
}
