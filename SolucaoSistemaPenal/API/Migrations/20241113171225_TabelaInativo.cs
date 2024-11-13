using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class TabelaInativo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DetentoInativoId",
                table: "TabelaDetentos");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "TabelaDetentos");

            migrationBuilder.AddColumn<string>(
                name: "DetentoInativoId",
                table: "TabelaAtividades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TabelaDetentosInativos",
                columns: table => new
                {
                    DetentoInativoId = table.Column<string>(type: "TEXT", nullable: false),
                    InicioPena = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FimPena = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CPF = table.Column<string>(type: "TEXT", nullable: true),
                    Sexo = table.Column<char>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabelaDetentosInativos", x => x.DetentoInativoId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TabelaAtividades_DetentoInativoId",
                table: "TabelaAtividades",
                column: "DetentoInativoId");

            migrationBuilder.AddForeignKey(
                name: "FK_TabelaAtividades_TabelaDetentosInativos_DetentoInativoId",
                table: "TabelaAtividades",
                column: "DetentoInativoId",
                principalTable: "TabelaDetentosInativos",
                principalColumn: "DetentoInativoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TabelaAtividades_TabelaDetentosInativos_DetentoInativoId",
                table: "TabelaAtividades");

            migrationBuilder.DropTable(
                name: "TabelaDetentosInativos");

            migrationBuilder.DropIndex(
                name: "IX_TabelaAtividades_DetentoInativoId",
                table: "TabelaAtividades");

            migrationBuilder.DropColumn(
                name: "DetentoInativoId",
                table: "TabelaAtividades");

            migrationBuilder.AddColumn<string>(
                name: "DetentoInativoId",
                table: "TabelaDetentos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "TabelaDetentos",
                type: "TEXT",
                maxLength: 21,
                nullable: false,
                defaultValue: "");
        }
    }
}
