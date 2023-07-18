using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
//DB
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


namespace CapaDatos
{
    public  class PersonaDAL
    {
        public List<PersonaCLS> listarPersona()
        {
            List<PersonaCLS> lista = null;
            string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspListarEmpleados", cn))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader drd = cmd.ExecuteReader();

                        if (drd != null)
                        {
                            lista = new List<PersonaCLS>();
                            PersonaCLS oPersonaCLS;

                            while (drd.Read())
                            {
                                oPersonaCLS = new PersonaCLS();
                                oPersonaCLS.noemp= drd.IsDBNull(0) ? 0 : drd.GetInt32(0);
                                oPersonaCLS.nombre = drd.IsDBNull(1) ? "" : drd.GetString(1);
                                oPersonaCLS.appaterno = drd.IsDBNull(2) ? "" : drd.GetString(2);
                                oPersonaCLS.apmaterno = drd.IsDBNull(3) ? "" : drd.GetString(3);
                                oPersonaCLS.puesto = drd.IsDBNull(4) ? "" : drd.GetString(4);
                                oPersonaCLS.correo = drd.IsDBNull(5) ? "" : drd.GetString(5);
                                oPersonaCLS.pass = drd.IsDBNull(6) ? "" : drd.GetString(6);
                                oPersonaCLS.direccion = drd.IsDBNull(7) ? "" : drd.GetString(7);
                                oPersonaCLS.area = drd.IsDBNull(8) ? "" : drd.GetString(8); 
                                lista.Add(oPersonaCLS);
                            }
                            
                        }

                    }


                }
                catch (Exception ex)
                {
                    lista = null;
                    cn.Close();
                }
            }
            return lista;

        }
    }
}
