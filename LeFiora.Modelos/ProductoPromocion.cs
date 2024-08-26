using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeFiora.Modelos
{
    public class ProductoPromocion
    {
        [Required]
        public int ProductoId { get; set; }
        [ForeignKey("ProductoId")]
        public Producto Producto { get; set; }
        [Required]
        public int PromocionId { get; set; }
        [ForeignKey("PromocionId")]
        public Promocion Promocion { get; set; }
    }
}
