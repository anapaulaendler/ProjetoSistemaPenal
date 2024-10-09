using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class MudandoNomeDasTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TabelaAtividade_TabelaPessoa_DetentoId",
                table: "TabelaAtividade");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TabelaPessoa",
                table: "TabelaPessoa");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TabelaAtividade",
                table: "TabelaAtividade");

            migrationBuilder.RenameTable(
                name: "TabelaPessoa",
                newName: "TabelaPessoas");

            migrationBuilder.RenameTable(
                name: "TabelaAtividade",
                newName: "TabelaAtividades");

            migrationBuilder.RenameIndex(
                name: "IX_TabelaAtividade_DetentoId",
                table: "TabelaAtividades",
                newName: "IX_TabelaAtividades_DetentoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TabelaPessoas",
                table: "TabelaPessoas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TabelaAtividades",
                table: "TabelaAtividades",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TabelaAtividades_TabelaPessoas_DetentoId",
                table: "TabelaAtividades",
                column: "DetentoId",
                principalTable: "TabelaPessoas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TabelaAtividades_TabelaPessoas_DetentoId",
                table: "TabelaAtividades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TabelaPessoas",
                table: "TabelaPessoas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TabelaAtividades",
                table: "TabelaAtividades");

            migrationBuilder.RenameTable(
                name: "TabelaPessoas",
                newName: "TabelaPessoa");

            migrationBuilder.RenameTable(
                name: "TabelaAtividades",
                newName: "TabelaAtividade");

            migrationBuilder.RenameIndex(
                name: "IX_TabelaAtividades_DetentoId",
                table: "TabelaAtividade",
                newName: "IX_TabelaAtividade_DetentoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TabelaPessoa",
                table: "TabelaPessoa",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TabelaAtividade",
                table: "TabelaAtividade",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TabelaAtividade_TabelaPessoa_DetentoId",
                table: "TabelaAtividade",
                column: "DetentoId",
                principalTable: "TabelaPessoa",
                principalColumn: "Id");
        }
    }
}
