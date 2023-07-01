using System;
using System.Collections.Generic;
using System.Text;

namespace SAVIVE.Clases
{
    public class SolicitudPasajesCLS
    {
        public int IdSolicitud { set; get; }
        public int IdDestino { set; get; }
        public string Transporte { set; get; }
        public decimal Tarifa { set; get; }
        public bool IdaVuelta { set; get; }

    }
}
