using LeFiora.AccesoDatos.Repositorio.IRepositorio;
using LeFiora.Modelos;
using LeFiora.Modelos.ViewModels;
using LeFiora.Utilidades;
using Microsoft.AspNetCore.Mvc;

namespace LeFiora.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductoController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductoController(IUnidadTrabajo unidadTrabajo, IWebHostEnvironment webHostEnvironment)
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
            ProductoVM productoVM = new ProductoVM()
            {
                Producto = new Producto(),
                CategoriaLista = _unidadTrabajo.Producto.ObtenerTodosDropDownList("Categoria"),
                CatalagoLista = _unidadTrabajo.Producto.ObtenerTodosDropDownList("Catalago"),
                PromocionLista = _unidadTrabajo.Producto.ObtenerTodosDropDownList("Promocion")
            };
            if (id == null)
            {
                return View(productoVM);
            }
            else
            {
                productoVM.Producto = await _unidadTrabajo.Producto.Obtener(id.GetValueOrDefault());
                if (productoVM.Producto == null)
                {
                    return NotFound();
                }
                return View(productoVM);
            }
        }

        #region API
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ProductoVM productoVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (productoVM.Producto.Id == 0)
                {
                    //crear un nuevo producto
                    string upload = webRootPath + DS.ImagenProductoRuta;
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

                    productoVM.Producto.ImagenURL = fileName + extension;
                    await _unidadTrabajo.Producto.Agregar(productoVM.Producto);
                }
                else
                {
                    //Actualizar al producto
                    var objProducto = await _unidadTrabajo.Producto
                                                .ObtenerPrimero(p => p.Id == productoVM.Producto.Id
                                                , isTracking: false);
                    if (files.Count > 0)
                    {
                        string upload = webRootPath + DS.ImagenProductoRuta;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        //borrar la imagen anterior 
                        var anteriorFile = Path.Combine(upload, objProducto.ImagenURL);

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

                        productoVM.Producto.ImagenURL = fileName + extension;

                    }// si no elige imagen 
                    else
                    {
                        productoVM.Producto.ImagenURL = objProducto.ImagenURL;
                    }
                    _unidadTrabajo.Producto.Actualizar(productoVM.Producto);
                }
                TempData[DS.Exitosa] = "Producto Registrado";
                await _unidadTrabajo.Guardar();
                return RedirectToAction("ProductoR", new {id = productoVM.Producto.Id});
            }
            TempData[DS.Error] = "Error al registrar el producto";
            productoVM.CategoriaLista = _unidadTrabajo.Producto.ObtenerTodosDropDownList("Categoria");
            productoVM.CatalagoLista = _unidadTrabajo.Producto.ObtenerTodosDropDownList("Catalago");
            productoVM.PromocionLista = _unidadTrabajo.Producto.ObtenerTodosDropDownList("Promocion");
            return View(productoVM);
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
