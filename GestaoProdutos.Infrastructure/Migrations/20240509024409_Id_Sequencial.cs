using Microsoft.EntityFrameworkCore.Migrations;

namespace GestaoProdutos.Infrastructure.Migrations
{
    public partial class Id_Sequencial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CNPJ",
                table: "Fornecedores",
                newName: "Cnpj");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cnpj",
                table: "Fornecedores",
                newName: "CNPJ");
        }
    }
}
