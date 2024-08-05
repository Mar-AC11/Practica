using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Practica.Models.TableModelCarrera
{
    public class CarreraModel
    {
        public int IdCarrera { get; set; }
        [Required]
        [StringLength(30)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El nombre de la carrera solo puede contener letras.")]
        [Display(Name = "Nombre de la carrera")]
        public string NomCarrera { get; set; }
        [Required]
        [StringLength(5)]
        [Display(Name = "Clave de la carrera")]
        public string Clave { get; set; }
        
    }
}