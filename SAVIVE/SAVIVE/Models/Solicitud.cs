using Android.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAVIVE.Models
{
    public class Solicitud
    {
        public int IdSolicitud{ get; set; }
        public string Autorizacion { get; set; }
        public string Justificacion{ get; set; }
        public DateTime Fecha { get; set; }
        public int Solicitante { get; set; }
        public int estado { set; get; }
        public string color { set; get; }

    }
}
