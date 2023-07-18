using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class UbicacionDAL
    {
        public List<UbicacionCLS> ListarUbicacion()
        {

            List<UbicacionCLS> lista = null;
            string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("ListarUbicaciones", cn))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader drd = cmd.ExecuteReader();

                        if (drd != null)
                        {
                            lista = new List<UbicacionCLS>();
                            UbicacionCLS oUbicacion;

                            while (drd.Read())
                            {
                                oUbicacion = new UbicacionCLS();
                                oUbicacion.Idubicacion = drd.IsDBNull(0) ? 0 : drd.GetInt32(0);
                                oUbicacion.IdSolicitud = drd.IsDBNull(1) ? 0: drd.GetInt32(1);
                                oUbicacion.IdEmpleado = drd.IsDBNull(2) ? 0 : drd.GetInt32(2);
                                oUbicacion.Fecha = drd.GetDateTime(3);
                                oUbicacion.Latitud = drd.IsDBNull(4) ? 0 : drd.GetDecimal(4);
                                oUbicacion.Longitud = drd.IsDBNull(5) ? 0 : drd.GetDecimal(5);
                                oUbicacion.Ubicacion = drd.IsDBNull(6) ? "" : drd.GetString(6);
                                lista.Add(oUbicacion);
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

        public int Subir_solicitud(UbicacionCLS oUbicacion)
        {
            int rpta = 0;
            string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("GuardarUbicacion", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDUBICACION", oUbicacion.Idubicacion);
                        cmd.Parameters.AddWithValue("@IDSOLICITUD", oUbicacion.IdSolicitud);
                        cmd.Parameters.AddWithValue("@IDEMPLEADO", oUbicacion.IdEmpleado);
                        cmd.Parameters.AddWithValue("@FECHA", oUbicacion.Fecha);
                        cmd.Parameters.AddWithValue("@LATITUD", oUbicacion.Latitud);
                        cmd.Parameters.AddWithValue("@LONGITUD", oUbicacion.Longitud);
                        cmd.Parameters.AddWithValue("@UBICACION", oUbicacion.Ubicacion);
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
