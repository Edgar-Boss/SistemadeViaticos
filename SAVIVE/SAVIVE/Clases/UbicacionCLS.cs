using System;
using System.Collections.Generic;
using System.Text;

namespace SAVIVE.Clases
{
    public class UbicacionCLS
    {
        public int Idubicacion { set; get; }
        public int IdSolicitud { set; get; }
        public int IdEmpleado { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public string Ubicacion { get; set; }


    }
}
