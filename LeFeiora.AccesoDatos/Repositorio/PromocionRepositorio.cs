using LeFiora.AccesoDatos.Repositorio.IRepositorio;
using LeFiora.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeFiora.AccesoDatos.Repositorio
{
    public class PromocionRepositorio : Repositorio<Promocion>, IPromocionRepositorio
    {
        private readonly ApplicationDbContext _db;

        public PromocionRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Promocion promocion)
        {
            var promocionBD = _db.Promociones.FirstOrDefault(c => c.Id == promocion.Id);
            if (promocionBD != null)
            {
                if (promocion.ImagenURL != null)
                {
                    promocionBD.ImagenURL = promocion.ImagenURL;
                }
                promocionBD.Nombre = promocion.Nombre;
                promocionBD.Descripcion = promocion.Descripcion;
                promocionBD.InHomePage = promocion.InHomePage;
                promocionBD.FechaInicio = promocion.FechaInicio;
                promocionBD.FechaFinal = promocionBD.FechaFinal;
                promocionBD.TipoDescuento = promocion.TipoDescuento;
                promocionBD.ValorDescuento = promocion.ValorDescuento;
                promocionBD.CantidadCompra = promocion.CantidadCompra;
                promocionBD.CantidadGratis = promocion.CantidadGratis;
                _db.SaveChanges();
            }
        }
    }
}
