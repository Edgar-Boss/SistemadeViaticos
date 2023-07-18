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
    public class AutorizarDAL
    {
        public List<AutorizarCLS> listarAutorizar()
        {
            List<AutorizarCLS> lista = null;
            string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("ListarUserAutorizar", cn))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader drd = cmd.ExecuteReader();

                        if (drd != null)
                        {
                            lista = new List<AutorizarCLS>();
                            AutorizarCLS oAutorizarCLS;

                            while (drd.Read())
                            {
                                oAutorizarCLS = new AutorizarCLS();
                                oAutorizarCLS.noemp = drd.IsDBNull(0) ? 0 : drd.GetInt32(0);
                                oAutorizarCLS.nombre = drd.IsDBNull(1) ? "" : drd.GetString(1);
                                oAutorizarCLS.appaterno = drd.IsDBNull(2) ? "" : drd.GetString(2);
                                oAutorizarCLS.apmaterno = drd.IsDBNull(3) ? "" : drd.GetString(3);
                                
                                lista.Add(oAutorizarCLS);
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
