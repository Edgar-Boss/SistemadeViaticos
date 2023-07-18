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
    public class DEmpleadoController : ApiController
    {
        public List<DEmpleadosCLS> Get()
        {
            DEmpleadosDAL oDEmpleados = new DEmpleadosDAL();
            return oDEmpleados.ListarDEmpleados();
        }

    }
}
