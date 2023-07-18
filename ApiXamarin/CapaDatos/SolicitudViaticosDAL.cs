using System;
using System.Collections.Generic;
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
    public class SolicitudViaticosDAL
    {

        public List<SolicitudViaticosCLS> ListarSolicitudViaticos()
        {

            List<SolicitudViaticosCLS> lista = null;

            string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("ListarSolicitudesViaticos", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader drd = cmd.ExecuteReader();

                        if (drd != null)
                        {
                            lista = new List<SolicitudViaticosCLS>();
                            SolicitudViaticosCLS oSolicitudCLS;
                            while (drd.Read())
                            {
                                oSolicitudCLS = new SolicitudViaticosCLS();
                                oSolicitudCLS.IdSolicitud = drd.IsDBNull(0) ? 0 : drd.GetInt32(0);
                                oSolicitudCLS.IdDestino = drd.IsDBNull(1) ? 0 : drd.GetInt32(1);
                                oSolicitudCLS.Zona = drd.IsDBNull(2) ? "" : drd.GetString(2);
                                oSolicitudCLS.Costo = drd.IsDBNull(3) ? 0 :  drd.GetDecimal(3);
                                oSolicitudCLS.Dias = drd.IsDBNull(4) ? 0 : drd.GetInt32(4);
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


        public int Subir_solicitud(SolicitudViaticosCLS oSolicitudVCLS)
        {
            int rpta = 0;
            string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("GuardarSolicitudViaticos", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idsolicitud", oSolicitudVCLS.IdSolicitud);
                        cmd.Parameters.AddWithValue("@iddestino", oSolicitudVCLS.IdDestino);
                        cmd.Parameters.AddWithValue("@zona", oSolicitudVCLS.Zona);
                        cmd.Parameters.AddWithValue("@costo", oSolicitudVCLS.Costo);
                        cmd.Parameters.AddWithValue("@dias", oSolicitudVCLS.Dias);
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
