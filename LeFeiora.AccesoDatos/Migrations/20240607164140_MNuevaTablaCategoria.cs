using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeFiora.AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class MNuevaTablaCategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "InHomePage",
                table: "Categorias",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InHomePage",
                table: "Categorias");
        }
    }
}
