using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeFiora.AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class TablaProductosYRelacionesMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ImagenURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Precio = table.Column<double>(type: "float", nullable: false),
                    TipoTamaño = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tamaño = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cantidad = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductosCatalagos",
                columns: table => new
                {
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    CatalagoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductosCatalagos", x => new { x.ProductoId, x.CatalagoId });
                    table.ForeignKey(
                        name: "FK_ProductosCatalagos_Catalagos_CatalagoId",
                        column: x => x.CatalagoId,
                        principalTable: "Catalagos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductosCatalagos_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductosCategorias",
                columns: table => new
                {
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductosCategorias", x => new { x.ProductoId, x.CategoriaId });
                    table.ForeignKey(
                        name: "FK_ProductosCategorias_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductosCategorias_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductosPromociones",
                columns: table => new
                {
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    PromocionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductosPromociones", x => new { x.ProductoId, x.PromocionId });
                    table.ForeignKey(
                        name: "FK_ProductosPromociones_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductosPromociones_Promociones_PromocionId",
                        column: x => x.PromocionId,
                        principalTable: "Promociones",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductosCatalagos_CatalagoId",
                table: "ProductosCatalagos",
                column: "CatalagoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosCategorias_CategoriaId",
                table: "ProductosCategorias",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosPromociones_PromocionId",
                table: "ProductosPromociones",
                column: "PromocionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductosCatalagos");

            migrationBuilder.DropTable(
                name: "ProductosCategorias");

            migrationBuilder.DropTable(
                name: "ProductosPromociones");

            migrationBuilder.DropTable(
                name: "Productos");
        }
    }
}
