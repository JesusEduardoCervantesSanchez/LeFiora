using LeFiora.AccesoDatos.Repositorio.IRepositorio;
using LeFiora.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeFiora.AccesoDatos.Repositorio
{
    public class CategoriaRepositorio: Repositorio<Categoria>, ICategoriaRepositorio
    {
        private readonly ApplicationDbContext _db;

        public CategoriaRepositorio(ApplicationDbContext db) : base (db)
        {
            _db = db;
        }

        public void Actualizar(Categoria categoria)
        {
            var categoriaBD = _db.Categorias.FirstOrDefault(c => c.Id == categoria.Id);
            if(categoriaBD != null)
            {
                if(categoria.ImagenURL != null)
                {
                    categoriaBD.ImagenURL = categoria.ImagenURL;
                }
                categoriaBD.Nombre = categoria.Nombre;
                categoriaBD.Descripcion=categoria.Descripcion;
                categoriaBD.InHomePage = categoria.InHomePage;
                _db.SaveChanges();
            }
        }
    }
}
