using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    /// <inheritdoc />
    public partial class AjustandoAsTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Produtos",
                newName: "ProdutoId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Categorias",
                newName: "CategoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProdutoId",
                table: "Produtos",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CategoriaId",
                table: "Categorias",
                newName: "Id");
        }
    }
}
