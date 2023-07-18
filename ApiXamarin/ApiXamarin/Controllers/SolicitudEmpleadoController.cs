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
    public class SolicitudEmpleadoController : ApiController
    {

        public List<SolicitudEmpleadoCLS> Get()
        {
            SolicitudEmpleadoDAL oSolicitudDAL = new SolicitudEmpleadoDAL();
            return oSolicitudDAL.ListarSolicitudEmpleado();

        }

        public int Post([FromBody] SolicitudEmpleadoCLS oSolicitudCLS)
        {
            SolicitudEmpleadoDAL oSolicitudVDAL = new SolicitudEmpleadoDAL();

            return oSolicitudVDAL.Subir_solicitud(oSolicitudCLS);

        }


    }
}
