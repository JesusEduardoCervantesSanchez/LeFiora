using LeFiora.AccesoDatos.Repositorio.IRepositorio;
using LeFiora.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeFiora.AccesoDatos.Repositorio
{
    public class FlorRepositorio : Repositorio<Flores>, IFlorRepositorio
    {
        private readonly ApplicationDbContext _db;
        public FlorRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Actualizar(Flores flor)
        {
            var florBD = _db.Flores.FirstOrDefault(f => f.Id == flor.Id);
            if (florBD != null)
            {
                florBD.Nombre = flor.Nombre;
                florBD.Precio = flor.Precio;
                florBD.Existencia = flor.Existencia;
                florBD.Descripcion = flor.Descripcion;

                _db.SaveChanges();
            }
        }
    }
}
