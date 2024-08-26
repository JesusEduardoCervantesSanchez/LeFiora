using LeFiora.AccesoDatos.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeFiora.AccesoDatos.Repositorio
{
    public class UnidadTrabajo : IUnidadTrabajo
    {
        private readonly ApplicationDbContext _db;

        public ICategoriaRepositorio Categoria { get; set; }
        public ICatalagoRepositorio Catalago { get; set; }
        public IPromocionRepositorio Promocion { get; set; }
        public IProductoRepositorio Producto { get; set; }

        public UnidadTrabajo(ApplicationDbContext db)
        {
            _db = db;
            Categoria = new CategoriaRepositorio(_db);
            Catalago = new CatalagoRepositorio(_db);
            Promocion = new PromocionRepositorio(_db);
        }


        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task Guardar()
        {
            await _db.SaveChangesAsync();
        }
    }
}
