using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CapaDatos;
using CapaEntidad;



namespace ApiXamarin.Controllers
{
    public class CategoriaController : ApiController
    {

        //public List<PersonaCLS> Get()
        //{
        //    PersonaDAL oPersonaDAL = new PersonaDAL();
        //    return oPersonaDAL.listarPersona();
        //}
        public List<CategoriaCLS> Get()
        {
            CategoriaDAL oCategoriaDAL = new CategoriaDAL();
            return oCategoriaDAL.listarCategoria();
        }
}
}
