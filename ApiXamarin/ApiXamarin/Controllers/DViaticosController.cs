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
    public class DViaticosController : ApiController
    {
        public List<DViaticoCLS> Get()
        { 
            DViaticosDAL oDViatico = new DViaticosDAL();
            return oDViatico.ListarViaticos();




        }
    
    }
}
