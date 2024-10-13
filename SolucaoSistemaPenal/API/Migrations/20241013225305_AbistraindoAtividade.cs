using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AbistraindoAtividade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TabelaAtividadesDetento_TabelaAtividades_EstudoId",
                table: "TabelaAtividadesDetento");

            migrationBuilder.DropForeignKey(
                name: "FK_TabelaAtividadesDetento_TabelaAtividades_LeituraId",
                table: "TabelaAtividadesDetento");

            migrationBuilder.DropForeignKey(
                name: "FK_TabelaAtividadesDetento_TabelaAtividades_TrabalhoId",
                table: "TabelaAtividadesDetento");

            migrationBuilder.DropIndex(
                name: "IX_TabelaAtividadesDetento_EstudoId",
                table: "TabelaAtividadesDetento");

            migrationBuilder.DropIndex(
                name: "IX_TabelaAtividadesDetento_LeituraId",
                table: "TabelaAtividadesDetento");

            migrationBuilder.DropColumn(
                name: "EstudoId",
                table: "TabelaAtividadesDetento");

            migrationBuilder.DropColumn(
                name: "LeituraId",
                table: "TabelaAtividadesDetento");

            migrationBuilder.DropColumn(
                name: "AnoAtual",
                table: "TabelaAtividades");

            migrationBuilder.DropColumn(
                name: "Contador",
                table: "TabelaAtividades");

            migrationBuilder.DropColumn(
                name: "Limite",
                table: "TabelaAtividades");

            migrationBuilder.RenameColumn(
                name: "TrabalhoId",
                table: "TabelaAtividadesDetento",
                newName: "AtividadeId");

            migrationBuilder.RenameIndex(
                name: "IX_TabelaAtividadesDetento_TrabalhoId",
                table: "TabelaAtividadesDetento",
                newName: "IX_TabelaAtividadesDetento_AtividadeId");

            migrationBuilder.AddColumn<int>(
                name: "Contador",
                table: "TabelaAtividadesDetento",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Equivalencia",
                table: "TabelaAtividades",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "REAL");

            migrationBuilder.AddForeignKey(
                name: "FK_TabelaAtividadesDetento_TabelaAtividades_AtividadeId",
                table: "TabelaAtividadesDetento",
                column: "AtividadeId",
                principalTable: "TabelaAtividades",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TabelaAtividadesDetento_TabelaAtividades_AtividadeId",
                table: "TabelaAtividadesDetento");

            migrationBuilder.DropColumn(
                name: "Contador",
                table: "TabelaAtividadesDetento");

            migrationBuilder.RenameColumn(
                name: "AtividadeId",
                table: "TabelaAtividadesDetento",
                newName: "TrabalhoId");

            migrationBuilder.RenameIndex(
                name: "IX_TabelaAtividadesDetento_AtividadeId",
                table: "TabelaAtividadesDetento",
                newName: "IX_TabelaAtividadesDetento_TrabalhoId");

            migrationBuilder.AddColumn<string>(
                name: "EstudoId",
                table: "TabelaAtividadesDetento",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LeituraId",
                table: "TabelaAtividadesDetento",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Equivalencia",
                table: "TabelaAtividades",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "AnoAtual",
                table: "TabelaAtividades",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Contador",
                table: "TabelaAtividades",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Limite",
                table: "TabelaAtividades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TabelaAtividadesDetento_EstudoId",
                table: "TabelaAtividadesDetento",
                column: "EstudoId");

            migrationBuilder.CreateIndex(
                name: "IX_TabelaAtividadesDetento_LeituraId",
                table: "TabelaAtividadesDetento",
                column: "LeituraId");

            migrationBuilder.AddForeignKey(
                name: "FK_TabelaAtividadesDetento_TabelaAtividades_EstudoId",
                table: "TabelaAtividadesDetento",
                column: "EstudoId",
                principalTable: "TabelaAtividades",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TabelaAtividadesDetento_TabelaAtividades_LeituraId",
                table: "TabelaAtividadesDetento",
                column: "LeituraId",
                principalTable: "TabelaAtividades",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TabelaAtividadesDetento_TabelaAtividades_TrabalhoId",
                table: "TabelaAtividadesDetento",
                column: "TrabalhoId",
                principalTable: "TabelaAtividades",
                principalColumn: "Id");
        }
    }
}
