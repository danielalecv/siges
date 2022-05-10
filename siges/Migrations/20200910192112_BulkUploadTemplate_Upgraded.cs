using Microsoft.EntityFrameworkCore.Migrations;

namespace siges.Migrations
{
    public partial class BulkUploadTemplate_Upgraded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TamanoArchivo",
                table: "BulkUploadTemplate",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TamanoArchivo",
                table: "BulkUploadTemplate");
        }
    }
}
