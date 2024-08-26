using LeFiora.AccesoDatos.Repositorio.IRepositorio;
using LeFiora.Modelos;
using LeFiora.Utilidades;
using Microsoft.AspNetCore.Mvc;

namespace LeFiora.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PromocionController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PromocionController(IUnidadTrabajo unidadTrabajo, IWebHostEnvironment webHostEnvironment)
        {
            _unidadTrabajo = unidadTrabajo;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Promocion promocion = new Promocion();
            if (id == null)
            {
                return View(promocion);
            }
            promocion = await _unidadTrabajo.Promocion.Obtener(id.GetValueOrDefault());
            if (promocion == null)
            {
                return NotFound();
            }
            return View(promocion);
        }

        #region API
        [HttpPost]
        public async Task<IActionResult> Upsert(Promocion promocion)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (promocion.Id == 0)
                {
                    //crear un nuevo producto
                    string upload = webRootPath + DS.ImagenPromocionRuta;
                    //Crear un id unico en mi sistema
                    string fileName = Guid.NewGuid().ToString();
                    //creamos una variable para conocer la extencion del archivo
                    string extension = Path.GetExtension(files[0].FileName);

                    //habilitar el filestream para crear el archivo de imagen en tiempo real 
                    using (var filestream = new FileStream(Path.Combine(upload, fileName + extension)
                                                           , FileMode.Create))
                    {
                        files[0].CopyTo(filestream);
                    }

                    promocion.FechaFinal = promocion.FechaFinal.AddHours(23).AddMinutes(59).AddSeconds(59);
                    promocion.ImagenURL = fileName + extension;
                    await _unidadTrabajo.Promocion.Agregar(promocion);
                }
                else
                {
                    //Actualizar al producto
                    var objPromocion = await _unidadTrabajo.Promocion
                                                .ObtenerPrimero(p => p.Id == promocion.Id
                                                , isTracking: false);
                    if (files.Count > 0)
                    {
                        string upload = webRootPath + DS.ImagenPromocionRuta;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        //borrar la imagen anterior 
                        var anteriorFile = Path.Combine(upload, objPromocion.ImagenURL);

                        //verificamos que la imagen exista

                        if (System.IO.File.Exists(anteriorFile))
                        {
                            System.IO.File.Delete(anteriorFile);
                        }

                        //creamos la nueva imagen
                        using (var filestream = new FileStream(
                                Path.Combine(upload, fileName + extension)
                                , FileMode.Create))
                        {
                            files[0].CopyTo(filestream);
                        }

                        promocion.FechaFinal = promocion.FechaFinal.AddHours(23).AddMinutes(59).AddSeconds(59);
                        promocion.ImagenURL = fileName + extension;

                    }// si no elige imagen 
                    else
                    {
                        promocion.ImagenURL = objPromocion.ImagenURL;
                    }
                    _unidadTrabajo.Promocion.Actualizar(promocion);
                }
                TempData[DS.Exitosa] = "Promocion Registrada";
                await _unidadTrabajo.Guardar();
                return View("Index");
            }
            return View(promocion);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var promocionDB = await _unidadTrabajo.Promocion.Obtener(id);
            if (promocionDB == null)
            {
                return Json(new { success = false, message = "Error al borrar el registro en la Base de datos" });
            }
            string upload = _webHostEnvironment.WebRootPath + DS.ImagenPromocionRuta;
            var anteriorFile = Path.Combine(upload, promocionDB.ImagenURL);
            if (System.IO.File.Exists(anteriorFile))
            {

                System.IO.File.Delete(anteriorFile);

            }
            _unidadTrabajo.Promocion.Remover(promocionDB);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Promocion eliminada con exito" });
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Promocion.ObtenerTodos();
            return Json(new {data=todos});
        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, int id = 0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Promocion.ObtenerTodos();

            if (id == 0)
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            }
            else
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim()
                                    == nombre.ToLower().Trim()
                                    && b.Id != id);
            }
            if (valor)
            {
                return Json(new { data = true });
            }
            return Json(new { data = false });
        }
        #endregion
    }
}
