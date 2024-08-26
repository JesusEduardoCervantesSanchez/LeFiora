using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeFiora.Modelos
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Se requiere el nombre")]
        [MaxLength(60, ErrorMessage = "El nombre solo se compone de 60 caracteres")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Se requiere la descripción")]
        [MaxLength(100, ErrorMessage = "La descripción se compone solo de 100 caracteres")]
        public string Descripcion { get; set; }
        public string ImagenURL { get; set; }
        [Required(ErrorMessage = "El precio es requerido")]
        [Range(0, int.MaxValue, ErrorMessage = "El precio debe ser mayor a cero")]
        public double? Precio { get; set; }
        [Required(ErrorMessage ="Por favor seleccione el tamaño")]
        public string TipoTamaño { get; set; }
        public string Tamaño { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
        public int? Cantidad { get; set; }
    }
}
