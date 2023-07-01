using System;
using System.Collections.Generic;
using System.Text;

namespace SAVIVE.Models
{
    public class DPasajaeCLS
    {
        public int Idsolicitud { get; set; }
        public int Iddestino { get; set; }
        public string Transporte { get; set; }
        public decimal Tarifa { get; set; }
        public bool Idavuelta { get; set; }
        public string Destino { get; set; }
        public string _Ida { get; set; }

    }
}
