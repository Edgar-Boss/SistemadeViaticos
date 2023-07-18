using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class SolicitudPasajesCLS
    {
        public int IdSolicitud { set; get; }
        public int IdDestino { set; get; }
        public string Transporte { set; get; }
        public decimal Tarifa { set; get; }
        public bool  IdaVuelta {set; get;}

    }
}
