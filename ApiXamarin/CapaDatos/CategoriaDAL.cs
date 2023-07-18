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
    public class CategoriaDAL
    {
        public List<CategoriaCLS> listarCategoria()
        {
            List<CategoriaCLS> lista = null;
            string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                { 
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspListarCategoria", cn))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader drd = cmd.ExecuteReader();

                        if (drd != null)
                        {
                            lista = new List<CategoriaCLS>();
                            CategoriaCLS oCategoriaCLS;

                            while (drd.Read())
                            {
                                oCategoriaCLS = new CategoriaCLS();
                                oCategoriaCLS.iidcategoria = drd.IsDBNull(0) ? 0 : drd.GetInt32(0);
                                oCategoriaCLS.nombre = drd.IsDBNull(1) ? "" : drd.GetString(1);
                                oCategoriaCLS.descripcion = drd.IsDBNull(2) ? "" : drd.GetString(2);
                                lista.Add(oCategoriaCLS);
                            }
                        }
                        
                    }
                
                
                }
                catch ( Exception ex)
                {    
                    lista = null;
                    cn.Close();
                }
            }
                return lista;
        
        }
    }

}
