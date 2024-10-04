using Models.Responses;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mappings.DatabaseConexion
{
    public class DatabaseConexionMapping
    {
        public Response ConnectDatabase()
        {
            Response response = new Response();
            try
            {
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                if ((connection.State) > 0)
                {
                    response.Code = "00";
                    response.Data = connection;
                }
                else
                {
                    response.Code = "01";
                    response.Message = "Fallo la conexion";
                }
            }
            catch (Exception ex)
            {
                response.Code = "01";
                response.Message = ex.Message;
                response.Data = null;
            }
            return response;
        }

        public async Task<Response> ConnectDatabaseAsync()
        {

            Response response = new Response();
            try
            {
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                await connection.OpenAsync();
                if ((connection.State) > 0)
                {
                    response.Code = "00";
                    response.Data = connection;
                }
                else
                {
                    response.Code = "01";
                    response.Message = "Fallo la conexion";
                }
            }
            catch (Exception ex)
            {
                response.Code = "01";
                response.Message = ex.Message;
                response.Data = null;
            }
            return response;
        }

    }
}
