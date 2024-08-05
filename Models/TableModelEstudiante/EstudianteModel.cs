using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Practica.Models.TableModelEstudiante
{
    public class EstudianteModel
    {
        public int IdEstudiante { get; set; }
        [Required]
        [StringLength(10)]
        [Display(Name = "Número de Control")]
        public string NoControl { get; set; }
        [Required]
        [StringLength(30)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El nombre solo puede contener letras.")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        [Required]
        [StringLength(30)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El apellido paterno solo puede contener letras.")]
        [Display(Name = "Apellido Paterno")]
        public string ApPaterno { get; set; }
        [Required]
        [StringLength(30)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El apellido materno solo puede contener letras.")]
        [Display(Name = "Apellido Materno")]
        public string ApMaterno { get; set; }
        [Required]
        [Display(Name = "Carrera")]
        public int Carreraa {  get; set; }
        [Display(Name = "Carrera")]
        public string NomCarrera { get; set; }

    }
}