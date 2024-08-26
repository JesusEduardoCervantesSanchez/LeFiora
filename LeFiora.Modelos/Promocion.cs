using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeFiora.Modelos
{
    public class Promocion
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
        [Required]
        public bool InHomePage { get; set; }
        [Required(ErrorMessage ="Se requiere la fecha de incio del catalago")]
        public DateTime FechaInicio { get; set; }
        [Required(ErrorMessage = "Se requiere la fecha del final del catalago")]
        public DateTime FechaFinal { get; set; }
        [Required(ErrorMessage = "Se requiere el tipo de descuento")]
        public string TipoDescuento { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "El descuento debe ser mayor a cero")]
        public double? ValorDescuento { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "La cantidad a comprar debe ser mayor a 0")]
        public int? CantidadCompra { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "La cantidad gratis debe ser mayor a 0")]
        public int? CantidadGratis { get; set; } 
    }
}
