using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class MudandoAlgunsNomesDeColunas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TabelaDetentos",
                newName: "DetentoId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TabelaAtividades",
                newName: "AtividadeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DetentoId",
                table: "TabelaDetentos",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AtividadeId",
                table: "TabelaAtividades",
                newName: "Id");
        }
    }
}
