using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Practica.Models.TableModelDocente
{
    public class DocenteTableModel
    {
        public int IdDocente { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string ApPaterno { get; set; }
        public string ApMaterno { get; set; }
        public int Asignatura { get; set; }
        public string NomAsignatura { get; set; }
    }
}