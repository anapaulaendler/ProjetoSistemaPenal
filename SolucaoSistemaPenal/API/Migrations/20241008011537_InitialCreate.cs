using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TabelaPessoa",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CPF = table.Column<string>(type: "TEXT", nullable: true),
                    Sexo = table.Column<string>(type: "TEXT", nullable: true),
                    Discriminator = table.Column<string>(type: "TEXT", maxLength: 8, nullable: false),
                    TempoPenaInicial = table.Column<int>(type: "INTEGER", nullable: true),
                    PenaRestante = table.Column<int>(type: "INTEGER", nullable: true),
                    InicioPena = table.Column<DateTime>(type: "TEXT", nullable: true),
                    FimPena = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabelaPessoa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TabelaAtividade",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Contador = table.Column<int>(type: "INTEGER", nullable: false),
                    Equivalencia = table.Column<float>(type: "REAL", nullable: false),
                    AnoAtual = table.Column<int>(type: "INTEGER", nullable: false),
                    Limite = table.Column<string>(type: "TEXT", nullable: true),
                    DetentoId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabelaAtividade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TabelaAtividade_TabelaPessoa_DetentoId",
                        column: x => x.DetentoId,
                        principalTable: "TabelaPessoa",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TabelaAtividade_DetentoId",
                table: "TabelaAtividade",
                column: "DetentoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TabelaAtividade");

            migrationBuilder.DropTable(
                name: "TabelaPessoa");
        }
    }
}
