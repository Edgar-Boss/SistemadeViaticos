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
    public class SolicitudDAL
    {

        public List<SolicitudCLS> ListarSolicitud()
        {

            List<SolicitudCLS> lista = null;
            string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("ListarSolicitudes", cn))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader drd = cmd.ExecuteReader();

                        if (drd != null)
                        {
                            lista = new List<SolicitudCLS>();
                            SolicitudCLS oSolicitudCLS;

                            while (drd.Read())
                            {
                                oSolicitudCLS = new SolicitudCLS();
                                oSolicitudCLS.idsolicitud = drd.IsDBNull(0) ? 0 : drd.GetInt32(0);
                                oSolicitudCLS.autorizacion = drd.IsDBNull(1) ? "" : drd.GetString(1);
                                oSolicitudCLS.justificacion = drd.IsDBNull(2) ? "" : drd.GetString(2);
                                oSolicitudCLS.fecha= drd.GetDateTime(3);
                                oSolicitudCLS.solicitante = drd.IsDBNull(4) ? 0 : drd.GetInt32(4);
                                oSolicitudCLS.estado = drd.IsDBNull(5) ? 0: drd.GetInt32(5);
                                oSolicitudCLS.total = drd.IsDBNull(6) ? 0 : drd.GetDecimal(6);
                                oSolicitudCLS.comprobado = drd.IsDBNull(7)? 0 : drd.GetDecimal(7);
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

        public int Subir_solicitud(SolicitudCLS oSolicitudCLS)
        {
            int rpta = 0;
            string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("GuardarSolicitud", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idsolicitud", oSolicitudCLS.idsolicitud);
                        cmd.Parameters.AddWithValue("@autorizacion", oSolicitudCLS.autorizacion);
                        cmd.Parameters.AddWithValue("@justificacion", oSolicitudCLS.justificacion);
                        cmd.Parameters.AddWithValue("@fecha", oSolicitudCLS.fecha);
                        cmd.Parameters.AddWithValue("@solicitante", oSolicitudCLS.solicitante);
                        cmd.Parameters.AddWithValue("@estado", oSolicitudCLS.estado);
                        cmd.Parameters.AddWithValue("@total", oSolicitudCLS.total);
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
