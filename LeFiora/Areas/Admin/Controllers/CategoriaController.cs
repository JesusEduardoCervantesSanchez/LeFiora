using LeFiora.AccesoDatos.Repositorio.IRepositorio;
using LeFiora.Modelos;
using LeFiora.Utilidades;
using Microsoft.AspNetCore.Mvc;

namespace LeFiora.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriaController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CategoriaController(IUnidadTrabajo unidadTrabajo, IWebHostEnvironment webHostEnvironment)
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
            Categoria categoria = new Categoria();
            if (id == null)
            {
                return View(categoria);
            }
            categoria = await _unidadTrabajo.Categoria.Obtener(id.GetValueOrDefault());
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        #region API
        [HttpPost]
        public async Task<IActionResult> Upsert(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (categoria.Id == 0)
                {
                    //crear un nuevo producto
                    string upload = webRootPath + DS.ImagenCategoriaRuta;
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

                    categoria.ImagenURL = fileName + extension;
                    await _unidadTrabajo.Categoria.Agregar(categoria);
                }
                else
                {
                    //Actualizar al producto
                    var objCategoria = await _unidadTrabajo.Categoria
                                                .ObtenerPrimero(c => c.Id == categoria.Id
                                                , isTracking: false);
                    if (files.Count > 0)
                    {
                        string upload = webRootPath + DS.ImagenCategoriaRuta;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        //borrar la imagen anterior 
                        var anteriorFile = Path.Combine(upload, objCategoria.ImagenURL);

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

                        categoria.ImagenURL = fileName + extension;

                    }// si no elige imagen 
                    else
                    {
                        categoria.ImagenURL = objCategoria.ImagenURL;
                    }
                    _unidadTrabajo.Categoria.Actualizar(categoria);
                }
                TempData[DS.Exitosa] = "Producto Registrado";
                await _unidadTrabajo.Guardar();
                return View("Index");
            }
            return View(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var categoriaDB = await _unidadTrabajo.Categoria.Obtener(id);
            if (categoriaDB == null)
            {
                return Json(new { success = false, message = "Error al borrar el registro en la Base de datos" });
            }
            string upload = _webHostEnvironment.WebRootPath + DS.ImagenCategoriaRuta;
            var anteriorFile = Path.Combine(upload, categoriaDB.ImagenURL);
            if (System.IO.File.Exists(anteriorFile))
            {

                System.IO.File.Delete(anteriorFile);

            }
            _unidadTrabajo.Categoria.Remover(categoriaDB);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Categoria eliminada con exito" });
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Categoria.ObtenerTodos();
            return Json(new {data=todos});
        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, int id = 0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Categoria.ObtenerTodos();

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
