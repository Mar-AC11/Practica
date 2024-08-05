using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Practica.Models.TableModelDocente
{
    public class DocenteModel
    {
        public int IdDocente { get; set; }
        [Required]
        [StringLength(8)]
        [Display(Name = "Cedula")]
        public string Cedula { get; set; }
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
        [Display(Name = "Asignatura")]
        public int Asignatura { get; set; }
        [Display(Name = "Asignatura")]
        public string NomAsignatura { get; set; }
    }
}