//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Practica.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class docente
    {
        public int IdDocente { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string ApPaterno { get; set; }
        public string ApMaterno { get; set; }
        public int Asignatura { get; set; }
    
        public virtual asignatura asignatura1 { get; set; }
    }
}
