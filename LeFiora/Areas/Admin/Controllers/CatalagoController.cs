using LeFiora.AccesoDatos.Repositorio.IRepositorio;
using LeFiora.Modelos;
using LeFiora.Utilidades;
using Microsoft.AspNetCore.Mvc;

namespace LeFiora.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CatalagoController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CatalagoController(IUnidadTrabajo unidadTrabajo, IWebHostEnvironment webHostEnvironment)
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
            Catalago catalago = new Catalago();
            if (id == null)
            {
                return View(catalago);
            }
            catalago = await _unidadTrabajo.Catalago.Obtener(id.GetValueOrDefault());
            if (catalago == null)
            {
                return NotFound();
            }
            return View(catalago);
        }

        #region API
        [HttpPost]
        public async Task<IActionResult> Upsert(Catalago catalago)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (catalago.Id == 0)
                {
                    //crear un nuevo producto
                    string upload = webRootPath + DS.ImagenCatalagoRuta;
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

                    catalago.FechaFinal = catalago.FechaFinal.AddHours(23).AddMinutes(59).AddSeconds(59);
                    catalago.ImagenURL = fileName + extension;
                    await _unidadTrabajo.Catalago.Agregar(catalago);
                }
                else
                {
                    //Actualizar al producto
                    var objCatalago = await _unidadTrabajo.Catalago
                                                .ObtenerPrimero(c => c.Id == catalago.Id
                                                , isTracking: false);
                    if (files.Count > 0)
                    {
                        string upload = webRootPath + DS.ImagenCatalagoRuta;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        //borrar la imagen anterior 
                        var anteriorFile = Path.Combine(upload, objCatalago.ImagenURL);

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

                        catalago.FechaFinal = catalago.FechaFinal.AddHours(23).AddMinutes(59).AddSeconds(59);
                        catalago.ImagenURL = fileName + extension;

                    }// si no elige imagen 
                    else
                    {
                        catalago.ImagenURL = objCatalago.ImagenURL;
                    }
                    _unidadTrabajo.Catalago.Actualizar(catalago);
                }
                TempData[DS.Exitosa] = "Catalago Registrado";
                await _unidadTrabajo.Guardar();
                return View("Index");
            }
            return View(catalago);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var catalagoDB = await _unidadTrabajo.Catalago.Obtener(id);
            if (catalagoDB == null)
            {
                return Json(new { success = false, message = "Error al borrar el registro en la Base de datos" });
            }
            string upload = _webHostEnvironment.WebRootPath + DS.ImagenCatalagoRuta;
            var anteriorFile = Path.Combine(upload, catalagoDB.ImagenURL);
            if (System.IO.File.Exists(anteriorFile))
            {

                System.IO.File.Delete(anteriorFile);

            }
            _unidadTrabajo.Catalago.Remover(catalagoDB);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Catalago eliminado con exito" });
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Catalago.ObtenerTodos();
            return Json(new {data=todos});
        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, int id = 0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Catalago.ObtenerTodos();

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
