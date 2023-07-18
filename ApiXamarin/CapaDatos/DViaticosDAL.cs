using CapaEntidad;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class DViaticosDAL
    {

        public List<DViaticoCLS> ListarViaticos()
        {
            List<DViaticoCLS> lista = null;
            string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DVIATICOS", cn))
                    { 
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader drd = cmd.ExecuteReader();
                        if (drd != null)
                        { 
                            lista = new List<DViaticoCLS>();
                            DViaticoCLS oDViaticos;
                            while (drd.Read())
                            {
                                oDViaticos = new DViaticoCLS();
                                oDViaticos.Idsolicitud = drd.IsDBNull(0) ? 0 : drd.GetInt32(0);
                                oDViaticos.Iddestino = drd.IsDBNull(1) ? 0 : drd.GetInt32(1);
                                oDViaticos.Zona = drd.IsDBNull(2) ? "" : drd.GetString(2);
                                oDViaticos.Costo = drd.IsDBNull(3) ? 0 : drd.GetDecimal(3);
                                oDViaticos.Dias = drd.IsDBNull(4) ? 0 : drd.GetInt32(4);
                                oDViaticos.Destino = drd.IsDBNull(5) ? "" : drd.GetString(5);
                                lista.Add(oDViaticos);
                            
                            
                            }

                        }

                    }
                }
                catch(Exception ex) 
                {
                    lista = null;
                    cn.Close();
                }
            
            }
            
            
            return lista;
        }
            

    }
}
