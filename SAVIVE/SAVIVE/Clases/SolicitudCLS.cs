using Android.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAVIVE.Clases
{
    public  class SolicitudCLS
    {
        public int idsolicitud { get; set; }
        public string autorizacion { get; set; }
        public string justificacion { get; set; }
        public DateTime fecha { set; get; }
        public int solicitante { set; get; }
        public int estado { set; get; }
        public decimal total { set; get; }
        public decimal comprobado { set; get; }



    }
}
