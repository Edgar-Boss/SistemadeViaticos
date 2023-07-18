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
    public class SolicitudController : ApiController
    {
        public List<SolicitudCLS> Get()
        {
            SolicitudDAL oSolicitudDAL = new SolicitudDAL();
            return oSolicitudDAL.ListarSolicitud();

        }
        //POST
        public int Post([FromBody] SolicitudCLS oSolicitudCLS)
        {
            SolicitudDAL oSolicitudDAL = new SolicitudDAL();
            return oSolicitudDAL.Subir_solicitud(oSolicitudCLS);
        }

    }
}
