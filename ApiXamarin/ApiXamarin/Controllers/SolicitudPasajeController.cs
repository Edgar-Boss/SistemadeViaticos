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
    public class SolicitudPasajeController : ApiController
    {
        public List<SolicitudPasajesCLS> Get()
        {
            SolicitudPasajesDAL oSolicitudDAL = new SolicitudPasajesDAL();
            return oSolicitudDAL.ListarSolicitudPasaje();



        }

        public int Post([FromBody] SolicitudPasajesCLS oSolicitudCLS)
        {
            SolicitudPasajesDAL oSolicitudVDAL = new SolicitudPasajesDAL();

            return oSolicitudVDAL.Subir_solicitud(oSolicitudCLS);

        }



    }
}
