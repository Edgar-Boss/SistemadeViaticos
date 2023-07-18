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
    public class DestinoController : ApiController
    {
        public List<DestinoCLS> Get()
        { 
            DestinoDAL oDestinoDAL = new DestinoDAL();
            return oDestinoDAL.listarDestino();
        }
    }
}
