using Mappings.DatabaseConexion;
using Models.Entities;
using Models.Helpers;
using Models.Queries;
using Models.Responses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Mappings.Usuario
{
    public class UsuarioMapping : IUsuarioMapping
    {
        public async Task<Response> RegistrarUsuarioMapping(Usuarios usuario)
        {
            Response response = new Response();
            DatabaseConexionMapping coneccion = new DatabaseConexionMapping();
            response = await coneccion.ConnectDatabaseAsync();
            if (response.Code != "00")
                return response;
            SqlConnection connectionSql = (SqlConnection)response.Data;
            SqlDataReader reader = null;
            try
            {
                SqlCommand command = new SqlCommand("sp_crear_usuario", connectionSql);
                string passwordHash = PasswordHasherHelper.HashPassword(usuario.Password);
                command.Parameters.Add(new SqlParameter("@username", SqlDbType.VarChar)).Value = usuario.UserName;
                command.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar)).Value = passwordHash;
                command.Parameters.Add(new SqlParameter("@id_persona", SqlDbType.Int)).Value = usuario.Id_Persona;
                command.CommandType = CommandType.StoredProcedure;

                reader = command.ExecuteReader();
                while (await reader.ReadAsync())
                {

                    response.Code = reader["Code"].ToString();
                    response.Data = reader["Data"].ToString();
                    response.Message = reader["Message"].ToString();
                }

                await reader.CloseAsync();
            }
            catch (Exception ex)
            {
                response.Code = "01";
                response.Message = ex.Message;
                response.Data = null;
            }
            finally
            {
                if (connectionSql.State > 0)
                {
                    await connectionSql.CloseAsync();
                }
            }
            return response;
        }

        public async Task<Response> BuscarUsuarioMapping(Login login)
        {
            Response response = new Response();
            DatabaseConexionMapping coneccion = new DatabaseConexionMapping();
            response = await coneccion.ConnectDatabaseAsync();
            SqlConnection connectionSql = (SqlConnection)response.Data;
            SqlDataReader reader = null;
            try
            {

                SqlCommand command = new SqlCommand($"SELECT top 1 * FROM Usuario WHERE username = '{login.UserName}'", connectionSql);
                reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    var itemUsuari = new Usuarios();

                    while (await reader.ReadAsync())
                    {
                        // Aquí procesas los datos del usuario, por ejemplo:
                        var id = reader["id"].ToString();
                        var username = reader["username"].ToString();
                        var password = reader["password"].ToString();
                        var id_persona = reader["id_persona"].ToString();

                       

                        itemUsuari.Id_Usuario = Convert.ToInt16(id);
                        itemUsuari.UserName = username;
                        itemUsuari.Password = password;
                        itemUsuari.Id_Persona = Convert.ToInt16(id_persona);
                        
                    }

                    response.Code = "00";
                    response.Message = "Usuario Encontrado";
                    response.Data = itemUsuari;
                }
                else
                {
                    response.Code = "01";
                    response.Message = "Usuario no encontrado";
                    response.Data = null;
                }

                // Cierra el lector una vez terminado
                await reader.CloseAsync();
            }
            catch (Exception ex)
            {
                response.Code = "01";
                response.Message = ex.Message;
                response.Data = null;
            }
            finally
            {
                if (connectionSql.State > 0) ;
                {
                    await connectionSql.CloseAsync();
                }
            }
            return response;

        }
    }
}
