using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace siges.Migrations
{
    public partial class EstructuraCustomVision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EstructuraCustomVision",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BoundingBoxHeight = table.Column<float>(nullable: false),
                    BoundingBoxLeft = table.Column<float>(nullable: false),
                    BoundingBoxTop = table.Column<float>(nullable: false),
                    BoundingBoxWidth = table.Column<float>(nullable: false),
                    Probability = table.Column<float>(nullable: false),
                    TagId = table.Column<string>(nullable: true),
                    TagName = table.Column<string>(nullable: true),
                    ArchivoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstructuraCustomVision", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstructuraCustomVision_Archivo_ArchivoId",
                        column: x => x.ArchivoId,
                        principalTable: "Archivo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EstructuraCustomVision_ArchivoId",
                table: "EstructuraCustomVision",
                column: "ArchivoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstructuraCustomVision");
        }
    }
}
