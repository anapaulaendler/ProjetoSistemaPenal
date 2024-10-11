using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class MudandoTabelaDetento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TabelaAtividades_TabelaPessoas_DetentoId",
                table: "TabelaAtividades");

            migrationBuilder.DropIndex(
                name: "IX_TabelaAtividades_DetentoId",
                table: "TabelaAtividades");

            migrationBuilder.DropColumn(
                name: "DetentoId",
                table: "TabelaAtividades");

            migrationBuilder.CreateTable(
                name: "TabelaAtividadesDetento",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    DetentoId = table.Column<string>(type: "TEXT", nullable: true),
                    LeituraId = table.Column<string>(type: "TEXT", nullable: true),
                    EstudoId = table.Column<string>(type: "TEXT", nullable: true),
                    TrabalhoId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabelaAtividadesDetento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TabelaAtividadesDetento_TabelaAtividades_EstudoId",
                        column: x => x.EstudoId,
                        principalTable: "TabelaAtividades",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TabelaAtividadesDetento_TabelaAtividades_LeituraId",
                        column: x => x.LeituraId,
                        principalTable: "TabelaAtividades",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TabelaAtividadesDetento_TabelaAtividades_TrabalhoId",
                        column: x => x.TrabalhoId,
                        principalTable: "TabelaAtividades",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TabelaAtividadesDetento_TabelaPessoas_DetentoId",
                        column: x => x.DetentoId,
                        principalTable: "TabelaPessoas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TabelaAtividadesDetento_DetentoId",
                table: "TabelaAtividadesDetento",
                column: "DetentoId");

            migrationBuilder.CreateIndex(
                name: "IX_TabelaAtividadesDetento_EstudoId",
                table: "TabelaAtividadesDetento",
                column: "EstudoId");

            migrationBuilder.CreateIndex(
                name: "IX_TabelaAtividadesDetento_LeituraId",
                table: "TabelaAtividadesDetento",
                column: "LeituraId");

            migrationBuilder.CreateIndex(
                name: "IX_TabelaAtividadesDetento_TrabalhoId",
                table: "TabelaAtividadesDetento",
                column: "TrabalhoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TabelaAtividadesDetento");

            migrationBuilder.AddColumn<string>(
                name: "DetentoId",
                table: "TabelaAtividades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TabelaAtividades_DetentoId",
                table: "TabelaAtividades",
                column: "DetentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_TabelaAtividades_TabelaPessoas_DetentoId",
                table: "TabelaAtividades",
                column: "DetentoId",
                principalTable: "TabelaPessoas",
                principalColumn: "Id");
        }
    }
}
