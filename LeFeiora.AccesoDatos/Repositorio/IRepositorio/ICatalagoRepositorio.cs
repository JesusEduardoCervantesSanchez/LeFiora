using LeFiora.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeFiora.AccesoDatos.Repositorio.IRepositorio
{
    public interface ICatalagoRepositorio : IRepositorio<Catalago>
    {
        void Actualizar(Catalago catalago);
    }
}
