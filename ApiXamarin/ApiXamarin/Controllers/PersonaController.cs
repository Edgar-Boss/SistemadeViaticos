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
    public class PersonaController : ApiController
    {
        public List<PersonaCLS> Get()
        {
            
            PersonaDAL oPersonaDAL = new PersonaDAL();
            return oPersonaDAL.listarPersona();
        
        }
    }
}
