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
    public class SolicitudViaticoController : ApiController
    {
        public List<SolicitudViaticosCLS> Get()
        {
            SolicitudViaticosDAL oSolicitudDAL = new SolicitudViaticosDAL();
            return oSolicitudDAL.ListarSolicitudViaticos();

        }

        

        public int Post([FromBody] SolicitudViaticosCLS oSolicitudCLS)
        {
            SolicitudViaticosDAL oSolicitudVDAL = new SolicitudViaticosDAL();

            return oSolicitudVDAL.Subir_solicitud(oSolicitudCLS);

        }

    }
}
