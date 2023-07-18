using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
//
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


namespace CapaDatos
{
    public class SolicitudPasajesDAL
    {

        public List<SolicitudPasajesCLS> ListarSolicitudPasaje()
        {

            List<SolicitudPasajesCLS> lista = null;

            string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("Listarsolicitudespasajes", cn))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader drd = cmd.ExecuteReader();

                        if (drd != null)
                        {
                            lista = new List<SolicitudPasajesCLS>();
                            SolicitudPasajesCLS oSolicitudCLS;
                            while (drd.Read())
                            {
                                oSolicitudCLS = new SolicitudPasajesCLS();
                                oSolicitudCLS.IdSolicitud = drd.IsDBNull(0) ? 0 : drd.GetInt32(0);
                                oSolicitudCLS.IdDestino= drd.IsDBNull(1) ? 0 : drd.GetInt32(1);
                                oSolicitudCLS.Transporte = drd.IsDBNull(2) ? "" : drd.GetString(2);
                                oSolicitudCLS.Tarifa = drd.IsDBNull(3) ? 0 : drd.GetDecimal(3);
                                oSolicitudCLS.IdaVuelta = drd.GetBoolean(4);
                                lista.Add(oSolicitudCLS);
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


        public int Subir_solicitud(SolicitudPasajesCLS oSolicitudPCLS)
        {
            int rpta = 0;
            string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("GuardarSolicitudPasajes", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idsolicitud", oSolicitudPCLS.IdSolicitud);
                        cmd.Parameters.AddWithValue("@iddestino", oSolicitudPCLS.IdDestino);
                        cmd.Parameters.AddWithValue("@transporte", oSolicitudPCLS.Transporte);
                        cmd.Parameters.AddWithValue("@tarifa", oSolicitudPCLS.Tarifa);
                        cmd.Parameters.AddWithValue("@idavuelta", oSolicitudPCLS.IdaVuelta);
                        rpta = cmd.ExecuteNonQuery();
                        cn.Close();
                    }
                }
                catch (Exception ex)
                {
                    cn.Close();
                    rpta = 0;
                }


            }


            return rpta;
        }






    }
}
