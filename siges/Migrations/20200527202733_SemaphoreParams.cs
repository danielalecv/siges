using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace siges.Migrations
{
    public partial class SemaphoreParams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SemaphoreParams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LlegadaVerde = table.Column<int>(nullable: false),
                    LlegadaAmarillo = table.Column<int>(nullable: false),
                    LlegadaRojo = table.Column<int>(nullable: false),
                    SalidaVerde = table.Column<int>(nullable: false),
                    SalidaAmarillo = table.Column<int>(nullable: false),
                    SalidaRojo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemaphoreParams", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SemaphoreParams");
        }
    }
}
