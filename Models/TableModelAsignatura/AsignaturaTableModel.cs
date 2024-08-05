using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Practica.Models.TableModelAsignatura
{
    public class AsignaturaTableModel
    {
        public int IdAsignatura { get; set; }
        public string NomAsignatura { get; set; }
        public int Carrera { get; set; }
        public string NomCarrera { get; set; }
    }
}