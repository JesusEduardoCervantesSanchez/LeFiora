using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeFiora.AccesoDatos.Repositorio.IRepositorio
{
    public interface IUnidadTrabajo : IDisposable
    {
        ICategoriaRepositorio Categoria {  get; }
        ICatalagoRepositorio Catalago { get; }
        IPromocionRepositorio Promocion { get; }
        IProductoRepositorio Producto { get; }
        Task Guardar();
    }
}
