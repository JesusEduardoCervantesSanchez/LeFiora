using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeFiora.Modelos
{
    public class ProductoCatalago
    {
        [Required]
        public int ProductoId { get; set; }
        [ForeignKey("ProductoId")]
        public Producto Producto { get; set; }
        [Required]
        public int CatalagoId { get; set; }
        [ForeignKey("CatalagoId")]
        public Catalago Catalago { get; set; }
    }
}
