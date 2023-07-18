using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CapaEntidad
{
    public class SolicitudCLS
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
