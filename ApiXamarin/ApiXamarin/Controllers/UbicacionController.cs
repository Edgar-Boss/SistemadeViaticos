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
    public class UbicacionController : ApiController
    {
        public List<UbicacionCLS> Get()
        {
            UbicacionDAL ubicacion = new UbicacionDAL();
            return ubicacion.ListarUbicacion();
        }

        public int Post([FromBody] UbicacionCLS oUbicacion)
        {
            UbicacionDAL oUbicacionDAL = new UbicacionDAL();

            return oUbicacionDAL.Subir_solicitud(oUbicacion);

        }

    }
}
