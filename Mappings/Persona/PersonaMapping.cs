using Mappings.DatabaseConexion;
using Models.Entities;
using Models.Responses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mappings.Persona
{
    public class PersonaMapping : IPersonaMapping
    {
        public async Task<Response> RegistrarPersona(Personas persona)
        {
            Response response = new Response();
            DatabaseConexionMapping coneccion = new DatabaseConexionMapping();
            response = await coneccion.ConnectDatabaseAsync();
            if (response.Code != "00")
                return response;
            SqlConnection coneccionSql = (SqlConnection)response.Data;
            SqlDataReader reader = null;
            try
            {
                SqlCommand command = new SqlCommand("sp_crear_persona", coneccionSql);
                command.Parameters.Add(new SqlParameter("@nombres", SqlDbType.VarChar)).Value = persona.Nombres;
                command.Parameters.Add(new SqlParameter("@apellidos", SqlDbType.VarChar)).Value = persona.Apellidos;
                command.Parameters.Add(new SqlParameter("email", SqlDbType.VarChar)).Value = persona.Email;
                command.Parameters.Add(new SqlParameter("@fechaNacimiento", SqlDbType.DateTime)).Value = persona.FechaNacimiento;
                command.CommandType = CommandType.StoredProcedure;

                reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    response.Data = reader["Data"].ToString();
                    response.Code = reader["Code"].ToString();
                    response.Message = reader["Message"].ToString();

                }

                await reader.CloseAsync();

            }
            catch (Exception ex)
            {
                response.Code = "01";
                response.Message = "PersonaMapping / RegistrarUsuario / " + ex.Message;
                response.Data = null;
            }
            finally
            {
                if (coneccionSql.State > 0)
                {
                    coneccionSql.Close();
                }
            }
            return response;
        }
    }
}
