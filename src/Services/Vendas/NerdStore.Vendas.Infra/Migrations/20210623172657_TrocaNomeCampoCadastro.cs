using Microsoft.EntityFrameworkCore.Migrations;

namespace NerdStore.Vendas.Infra.Migrations
{
    public partial class TrocaNomeCampoCadastro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "Vouchers",
                newName: "DataCadastro");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataCadastro",
                table: "Vouchers",
                newName: "DataCriacao");
        }
    }
}
