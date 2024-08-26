using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeFiora.AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class TablaPromocionesMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Promociones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ImagenURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InHomePage = table.Column<bool>(type: "bit", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFinal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoDescuento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorDescuento = table.Column<double>(type: "float", nullable: true),
                    CantidadCompra = table.Column<int>(type: "int", nullable: true),
                    CantidadGratis = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promociones", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Promociones");
        }
    }
}
