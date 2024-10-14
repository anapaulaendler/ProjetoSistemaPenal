using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class adicionandoTabelasDeAtividades4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TabelaAtividades",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Contador = table.Column<int>(type: "INTEGER", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    Limite = table.Column<int>(type: "INTEGER", nullable: true),
                    AnoAtual = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabelaAtividades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TabelaDetentos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    TempoPenaInicial = table.Column<int>(type: "INTEGER", nullable: false),
                    PenaRestante = table.Column<int>(type: "INTEGER", nullable: false),
                    InicioPena = table.Column<string>(type: "TEXT", nullable: true),
                    FimPena = table.Column<string>(type: "TEXT", nullable: true),
                    EstudoId = table.Column<string>(type: "TEXT", nullable: false),
                    LeituraId = table.Column<string>(type: "TEXT", nullable: false),
                    TrabalhoId = table.Column<string>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    DataNascimento = table.Column<string>(type: "TEXT", nullable: true),
                    CPF = table.Column<string>(type: "TEXT", nullable: true),
                    Sexo = table.Column<char>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabelaDetentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TabelaDetentos_TabelaAtividades_EstudoId",
                        column: x => x.EstudoId,
                        principalTable: "TabelaAtividades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TabelaDetentos_TabelaAtividades_LeituraId",
                        column: x => x.LeituraId,
                        principalTable: "TabelaAtividades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TabelaDetentos_TabelaAtividades_TrabalhoId",
                        column: x => x.TrabalhoId,
                        principalTable: "TabelaAtividades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TabelaDetentos_EstudoId",
                table: "TabelaDetentos",
                column: "EstudoId");

            migrationBuilder.CreateIndex(
                name: "IX_TabelaDetentos_LeituraId",
                table: "TabelaDetentos",
                column: "LeituraId");

            migrationBuilder.CreateIndex(
                name: "IX_TabelaDetentos_TrabalhoId",
                table: "TabelaDetentos",
                column: "TrabalhoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TabelaDetentos");

            migrationBuilder.DropTable(
                name: "TabelaAtividades");
        }
    }
}
