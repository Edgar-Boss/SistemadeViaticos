using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public  class DEmpleadosDAL
    {
        public List<DEmpleadosCLS> ListarDEmpleados()
        {
            List<DEmpleadosCLS> lista = null;
            string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                { 
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DLISTAREMPLEADOS", cn))
                    {
                        cmd.CommandType  = CommandType.StoredProcedure;
                        SqlDataReader drd = cmd.ExecuteReader();

                        if (drd != null)
                        { 
                            lista = new List<DEmpleadosCLS>();
                            DEmpleadosCLS oDempleados;

                            while (drd.Read())
                            {
                                oDempleados = new DEmpleadosCLS();
                                oDempleados.idsolicitud = drd.IsDBNull(0) ? 0 : drd.GetInt32(0);
                                oDempleados.Autorizacion = drd.IsDBNull(1) ? "":drd.GetString(1);
                                oDempleados.Justifiacion = drd.IsDBNull(2) ? "" : drd.GetString(2);
                                oDempleados.Fecha = drd.GetDateTime(3);
                                oDempleados.Solicitante = drd.IsDBNull(4) ? 0 : drd.GetInt32(4);
                                oDempleados.Estado = drd.IsDBNull(5) ? 0 : drd.GetInt32(5);
                                oDempleados.Idempleado = drd.IsDBNull(6) ? 0 : drd.GetInt32(6);
                                oDempleados.DJustificacion = drd.IsDBNull(7) ? "" : drd.GetString(7);
                                oDempleados.Nombre = drd.IsDBNull(8) ? "" : drd.GetString(8);
                                oDempleados.Paterno = drd.IsDBNull(9) ? "" : drd.GetString(9);
                                oDempleados.Materno = drd.IsDBNull(10) ? "" : drd.GetString(10);
                                oDempleados.Puesto = drd.IsDBNull(11) ? "" : drd.GetString(11);
                                oDempleados.Area = drd.IsDBNull(12) ? "" : drd.GetString(12);
                                lista.Add(oDempleados);

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
