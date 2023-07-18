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
    public class SolicitudEmpleadoDAL
    {
        public List<SolicitudEmpleadoCLS> ListarSolicitudEmpleado()
        {

            List<SolicitudEmpleadoCLS> lista = null;

            string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("ListarSolicitudEmpleados", cn))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader drd = cmd.ExecuteReader();

                        if (drd != null)
                        {
                            lista = new List<SolicitudEmpleadoCLS>();
                            SolicitudEmpleadoCLS oSolicitudCLS;
                            while (drd.Read())
                            {
                                oSolicitudCLS = new SolicitudEmpleadoCLS();
                                oSolicitudCLS.IdSolicitud = drd.IsDBNull(0) ? 0 : drd.GetInt32(0);
                                oSolicitudCLS.IdEmpleado = drd.IsDBNull(1) ? 0 : drd.GetInt32(1);
                                oSolicitudCLS.Justificacion = drd.IsDBNull(2) ? "" : drd.GetString(2);

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


        public int Subir_solicitud(SolicitudEmpleadoCLS oSolicitudECLS)
        {
            int rpta = 0;
            string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("GuardarSolicitudEmpleados", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idsolicitud", oSolicitudECLS.IdSolicitud);
                        cmd.Parameters.AddWithValue("@idempleado", oSolicitudECLS.IdEmpleado);
                        cmd.Parameters.AddWithValue("@justificacion", oSolicitudECLS.Justificacion );
                        
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
