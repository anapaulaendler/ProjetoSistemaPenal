using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TabelaDetentos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    TempoPenaInicial = table.Column<int>(type: "INTEGER", nullable: false),
                    PenaRestante = table.Column<int>(type: "INTEGER", nullable: false),
                    InicioPena = table.Column<string>(type: "TEXT", nullable: true),
                    FimPena = table.Column<string>(type: "TEXT", nullable: true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    DataNascimento = table.Column<string>(type: "TEXT", nullable: true),
                    CPF = table.Column<string>(type: "TEXT", nullable: true),
                    Sexo = table.Column<char>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabelaDetentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TabelaAtividades",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    DetentoId = table.Column<string>(type: "TEXT", nullable: false),
                    Contador = table.Column<int>(type: "INTEGER", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    Limite = table.Column<int>(type: "INTEGER", nullable: true),
                    AnoAtual = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabelaAtividades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TabelaAtividades_TabelaDetentos_DetentoId",
                        column: x => x.DetentoId,
                        principalTable: "TabelaDetentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TabelaAtividades_DetentoId",
                table: "TabelaAtividades",
                column: "DetentoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TabelaAtividades");

            migrationBuilder.DropTable(
                name: "TabelaDetentos");
        }
    }
}
