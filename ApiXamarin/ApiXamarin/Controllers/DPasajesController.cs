using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiXamarin.Controllers
{
    public class DPasajesController : ApiController
    {
        public List<DPasajaeCLS> Get()
        { 
            DPasajeDAL oDPasajes = new DPasajeDAL();
            return oDPasajes.LsitarDPasajes();
        }
    }
}
