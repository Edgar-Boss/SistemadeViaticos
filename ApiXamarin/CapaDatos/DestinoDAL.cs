using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using CapaEntidad;

namespace CapaDatos
{
    public class DestinoDAL
    {
        public List<DestinoCLS> listarDestino()
        {
            List<DestinoCLS> lista = null;
            string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspListarDestinos", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader drd = cmd.ExecuteReader();

                        if (drd != null)
                        {
                            lista = new List<DestinoCLS>();
                            DestinoCLS oDestinoCLS;

                            while (drd.Read())
                            {  
                                oDestinoCLS = new DestinoCLS();
                                oDestinoCLS.iddestino = drd.IsDBNull(0) ? 0 : drd.GetInt32(0);
                                oDestinoCLS.destino = drd.IsDBNull(1) ? "" : drd.GetString(1);
                                oDestinoCLS.costo = drd.IsDBNull(2) ? 0 : drd.GetDecimal(2);
                                oDestinoCLS.autobus = drd.IsDBNull(3) ? 0: drd.GetDecimal(3); 
                                lista.Add(oDestinoCLS);
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
