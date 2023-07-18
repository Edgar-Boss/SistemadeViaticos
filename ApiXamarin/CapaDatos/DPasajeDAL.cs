using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class DPasajeDAL
    {

        public List<DPasajaeCLS> LsitarDPasajes()
        {

            List<DPasajaeCLS> lista = null;
            string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DLISTARPASAJES", cn))
                    { 
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader drd = cmd.ExecuteReader();

                        if (drd != null)
                        {
                            lista = new List<DPasajaeCLS>();
                            DPasajaeCLS oDPasajes;
                            while(drd.Read())
                            {
                                oDPasajes = new DPasajaeCLS();
                                oDPasajes.Idsolicitud = drd.IsDBNull(0) ? 0 : drd.GetInt32(0);
                                oDPasajes.Iddestino = drd.IsDBNull(1) ? 0 : drd.GetInt32(1);
                                oDPasajes.Transporte = drd.IsDBNull(2) ? "" : drd.GetString(2);
                                oDPasajes.Tarifa = drd.IsDBNull(3) ? 0: drd.GetDecimal(3);
                                oDPasajes.Idavuelta = drd.IsDBNull(4) ? false:drd.GetBoolean(4);
                                oDPasajes.Destino = drd.IsDBNull(5) ? "" : drd.GetString(5);
                                lista.Add(oDPasajes);


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
