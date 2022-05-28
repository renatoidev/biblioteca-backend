using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace biblioteca_backend.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Obra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    titulo = table.Column<string>(type: "VARCHAR", maxLength: 45, nullable: false),
                    editora = table.Column<string>(type: "VARCHAR", maxLength: 45, nullable: false),
                    foto = table.Column<string>(type: "VARCHAR", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obra", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ObraAutor",
                columns: table => new
                {
                    AutorId = table.Column<int>(type: "INTEGER", nullable: false),
                    ObraId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObraAutor", x => new { x.AutorId, x.ObraId });
                    table.ForeignKey(
                        name: "FK_ObraAutor_AutorId",
                        column: x => x.AutorId,
                        principalTable: "Obra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ObraAutor_ObraId",
                        column: x => x.ObraId,
                        principalTable: "Autor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ObraAutor_ObraId",
                table: "ObraAutor",
                column: "ObraId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ObraAutor");

            migrationBuilder.DropTable(
                name: "Obra");

            migrationBuilder.DropTable(
                name: "Autor");
        }
    }
}
