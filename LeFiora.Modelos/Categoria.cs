﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeFiora.Modelos
{
    public class Categoria
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
    }
}
