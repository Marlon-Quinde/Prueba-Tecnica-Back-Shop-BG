using Mappings.Persona;
using Mappings.Usuario;
using Models.Entities;
using Models.Helpers;
using Models.Queries;
using Models.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Usuario
{
    public class UsuarioBusiness : IUsuarioBusiness
    {
        private readonly IUsuarioMapping _usuarioMapping;
        private readonly IPersonaMapping _personaMapping;
        public UsuarioBusiness(IUsuarioMapping usuarioMapping, IPersonaMapping personaMapping)
        {
            this._usuarioMapping = usuarioMapping;
            this._personaMapping = personaMapping;
        }

        public async Task<Response> RegistarUsuarioBusiness(PersonaUsuarioQuery query)
        {
            Response response = new Response();

            try
            {
                var personaQuery = JsonConvert.SerializeObject(query);
                var persona = JsonConvert.DeserializeObject<Personas>(personaQuery);
                response = await _personaMapping.RegistrarPersona(persona);

                if (response.Code != "00")
                    return response;

                query.Id_Persona = int.Parse(response.Data.ToString());
                var usuarioQuery = JsonConvert.SerializeObject(query);
                var usuario = JsonConvert.DeserializeObject<Usuarios>(usuarioQuery);
                response = await _usuarioMapping.RegistrarUsuarioMapping(usuario);

                if (response.Code != "00")
                    return response;

                response.Data = null;
                response.Message = "Usuario creado con exito";
            }
            catch (Exception ex)
            {
                response.Code = "01";
                response.Message = "UsuarioBusiness / RegistarUsuarioBusiness / " + ex.Message;
                response.Data = null;
            }
            return response;
        }

        public async Task<Response> LoginUsuarioBusiness(Login query)
        {
            Response response = new Response();
            try
            {
                response = await _usuarioMapping.BuscarUsuarioMapping(query);

                Usuarios res = (Usuarios)response.Data;

                response.Data = PasswordHasherHelper.VerifyPassword(res.Password, query.Password);
            }
            catch (Exception ex)
            {
                response.Code = "01";
                response.Message = "UsuarioBusiness / LoginUsuarioBusiness / " + ex.Message;
                response.Data = null;
            }

            return response;
        }
    }
}
