using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class SolicitudViaticosCLS
    {
        public int IdSolicitud { set; get; }
        public int IdDestino { set; get; }
        public string Zona { get; set; }
        public decimal Costo { get; set; }
        public int Dias { get; set; }
    }
}
