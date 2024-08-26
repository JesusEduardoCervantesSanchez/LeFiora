using LeFiora.AccesoDatos.Repositorio.IRepositorio;
using LeFiora.Modelos.ViewModels;
using Microsoft.AspNetCore.Mvc;
using SistemaInventario.Modelos.Especificaciones;
using System.Diagnostics;

namespace LeFiora.Areas.Inventario.Controllers
{
    [Area("Inventario")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnidadTrabajo _unidadTrabajo;

        public HomeController(ILogger<HomeController> logger, IUnidadTrabajo unidadTrabajo)
        {
            _logger = logger;
            _unidadTrabajo = unidadTrabajo;
        }

        public IActionResult Index()
        {

            var categorias = _unidadTrabajo.Categoria.ObtenerTodosLista(c => c.InHomePage==true);
            var catalagos = _unidadTrabajo.Catalago.ObtenerTodosLista(c => c.InHomePage == true 
                                                                                    && c.FechaInicio<DateTime.Now 
                                                                                    && c.FechaFinal>DateTime.Now);
            var promociones = _unidadTrabajo.Promocion.ObtenerTodosLista(p =>  p.InHomePage == true
                                                                                    && p.FechaInicio < DateTime.Now
                                                                                    && p.FechaFinal > DateTime.Now);

            var hpageitems = new List<HomeViewModel>();

            hpageitems.AddRange(categorias.Select(s => new HomeViewModel
            {
                Tipo="categorias",
                ImagenURL = s.ImagenURL,
                Nombre = s.Nombre
            }));

            hpageitems.AddRange(catalagos.Select(s => new HomeViewModel
            {
                Tipo = "catalagos",
                ImagenURL = s.ImagenURL,
                Nombre = s.Nombre
            }));

            hpageitems.AddRange(promociones.Select(s => new HomeViewModel
            {
                Tipo = "promociones",
                ImagenURL = s.ImagenURL,
                Nombre = s.Nombre
            }));

            return View(hpageitems);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
