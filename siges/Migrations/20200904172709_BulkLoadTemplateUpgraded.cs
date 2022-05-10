using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace siges.Migrations
{
    public partial class BulkLoadTemplateUpgraded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreadoPorId",
                table: "BulkUploadTemplate",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaAdministrativa",
                table: "BulkUploadTemplate",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Version",
                table: "BulkUploadTemplate",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BulkUploadTemplate_CreadoPorId",
                table: "BulkUploadTemplate",
                column: "CreadoPorId");

            migrationBuilder.AddForeignKey(
                name: "FK_BulkUploadTemplate_AspNetUsers_CreadoPorId",
                table: "BulkUploadTemplate",
                column: "CreadoPorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BulkUploadTemplate_AspNetUsers_CreadoPorId",
                table: "BulkUploadTemplate");

            migrationBuilder.DropIndex(
                name: "IX_BulkUploadTemplate_CreadoPorId",
                table: "BulkUploadTemplate");

            migrationBuilder.DropColumn(
                name: "CreadoPorId",
                table: "BulkUploadTemplate");

            migrationBuilder.DropColumn(
                name: "FechaAdministrativa",
                table: "BulkUploadTemplate");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "BulkUploadTemplate");
        }
    }
}
