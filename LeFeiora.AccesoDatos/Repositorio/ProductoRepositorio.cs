using LeFiora.AccesoDatos.Repositorio.IRepositorio;
using LeFiora.Modelos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeFiora.AccesoDatos.Repositorio
{
    public class ProductoRepositorio : Repositorio<Producto>, IProductoRepositorio
    {
        private readonly ApplicationDbContext _db;

        public ProductoRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Producto producto)
        {
            var productoBD = _db.Productos.FirstOrDefault(p => p.Id == producto.Id);
            if (productoBD != null)
            {
                if (producto.ImagenURL != null)
                {
                    productoBD.ImagenURL = producto.ImagenURL;
                }
                productoBD.Nombre = producto.Nombre;
                productoBD.Descripcion = producto.Descripcion;
                productoBD.Precio = producto.Precio;
                productoBD.TipoTamaño = productoBD.TipoTamaño;
                productoBD.Tamaño = producto.Tamaño;
                productoBD.Cantidad = producto.Cantidad;
                _db.SaveChanges();
            }
        }
        public IEnumerable<SelectListItem> ObtenerTodosDropDownList(string obj)
        {
            if (obj == "Categoria")
            {
                return _db.Categorias.Where(c => c.Id>0).Select(c => new SelectListItem
                {
                    Text = c.Nombre,
                    Value = c.Id.ToString()
                });
            }
            if (obj == "Catalago")
            {
                return _db.Catalagos.Where(c => c.Id > 0).Select(c => new SelectListItem
                {
                    Text = c.Nombre,
                    Value = c.Id.ToString()
                });
            }
            if (obj == "Promociones")
            {
                return _db.Promociones.Where(c => c.Id > 0).Select(c => new SelectListItem
                {
                    Text = c.Descripcion,
                    Value = c.Id.ToString()
                });
            }
            return null;
        }
    }
}
