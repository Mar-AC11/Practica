using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Practica.Models.TableModelEstudiante
{
    public class EstudianteTableModel
    {
        public int IdEstudiante { get; set; }
        public string NoControl { get; set; }
        public string Nombre { get; set; }
        public string ApPaterno { get; set; }
        public string ApMaterno { get; set; }
        public int Carreraa { get; set; }
        public string NomCarrera { get; set; }
    }
}