using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeFiora.Modelos
{
    public class Flores
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Es obligatatrio ingresar el nombre de la flor")]
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public int Existencia { get; set; }
        public string Descripcion { get; set; }
    }
}
