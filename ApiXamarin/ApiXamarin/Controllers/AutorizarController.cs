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
    public class AutorizarController : ApiController
    {
        public List<AutorizarCLS> Get()
        { 
            AutorizarDAL oAutorizarDAL  = new AutorizarDAL();
            return oAutorizarDAL.listarAutorizar();
        }
    }
}
