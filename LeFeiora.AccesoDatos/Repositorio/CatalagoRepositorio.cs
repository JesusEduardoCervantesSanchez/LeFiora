using LeFiora.AccesoDatos.Repositorio.IRepositorio;
using LeFiora.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeFiora.AccesoDatos.Repositorio
{
    public class CatalagoRepositorio : Repositorio<Catalago>, ICatalagoRepositorio
    {
        private readonly ApplicationDbContext _db;

        public CatalagoRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Catalago catalago)
        {
            var catalagoBD = _db.Catalagos.FirstOrDefault(c => c.Id == catalago.Id);
            if (catalagoBD != null)
            {
                if (catalago.ImagenURL != null)
                {
                    catalagoBD.ImagenURL = catalago.ImagenURL;
                }
                catalagoBD.Nombre = catalago.Nombre;
                catalagoBD.Descripcion = catalago.Descripcion;
                catalagoBD.InHomePage = catalago.InHomePage;
                catalagoBD.FechaInicio = catalago.FechaInicio;
                catalagoBD.FechaFinal = catalago.FechaFinal;
                _db.SaveChanges();
            }
        }
    }
}
