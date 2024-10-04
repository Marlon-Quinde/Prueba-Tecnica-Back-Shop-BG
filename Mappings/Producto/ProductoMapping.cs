using Mappings.DatabaseConexion;
using Models.Responses;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Queries;

namespace Mappings.Producto
{
    public class ProductoMapping : IProductoMapping
    {
        public async Task<Response> GetProducts()
        {
            Response response = new Response();
            DatabaseConexionMapping conneccion = new DatabaseConexionMapping();
            response = conneccion.ConnectDatabase();
            if (response.Code != "00")
                return response;
            SqlConnection conneccionSql = (SqlConnection)response.Data;
            SqlDataReader reader = null;

            List<ListadoProducto> productos = new List<ListadoProducto>();
            try
            {
                SqlCommand command = new("sp_listar_producto", conneccionSql);
                //command.Parameters.Add(new SqlParameter("@pgc_plan_cuenta_Type", SqlDbType.Structured)).Value = dataTable;
                //command.Parameters.Add(new SqlParameter("@usuario", SqlDbType.VarChar)).Value = usuario;

                command.CommandType = CommandType.StoredProcedure;
                reader = await command.ExecuteReaderAsync();
                response.Code = "00";
                while (await reader.ReadAsync())
                {
                    productos.Add(new ListadoProducto() { 
                        Id = int.Parse(reader["Id"].ToString()), 
                        Nombre = reader["Nombre"].ToString(), 
                        Precio = decimal.Parse(reader["Precio"].ToString()), 
                        Stock = int.Parse(reader["Stock"].ToString()),
                        Nombre_Categoria = reader["Nombre_Categoria"].ToString(),

                    });
                }
                response.Message = "Todo OK";
                response.Data = productos;
            }
            catch (Exception ex)
            {
                response.Code = "01";
                response.Message = ex.Message;
                response.Data = null;
            }
            finally
            {
                if ((conneccionSql.State) > 0)
                {
                    await conneccionSql.CloseAsync();
                }
            }
            return response;
        }
    }
}
