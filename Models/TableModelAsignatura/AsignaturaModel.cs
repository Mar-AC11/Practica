using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Practica.Models.TableModelAsignatura
{
    public class AsignaturaModel
    {
        public int IdAsignatura { get; set; }
        [Required]
        [StringLength(10)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El nombre solo puede contener letras.")]
        [Display(Name = "Nombre de la asignatura")]
        public string NomAsignatura { get; set; }
        [Required]
        [Display(Name = "Carrera")]
        public int Carrera { get; set; }
        [Display(Name = "Carrera")]
        public string NomCarrera { get; set; }
    }
}