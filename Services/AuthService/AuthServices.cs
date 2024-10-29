using DataContext;
using Helpers;
using Microsoft.EntityFrameworkCore;
using Models.DTO.Auth;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.AuthService
{
    public class AuthServices
    {
        private readonly ShopContext _shopContext;
        public AuthServices(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }
        public async Task<Response<string>> LoginService(LoginDTO payload) 
        {
            try
            {
                Persona? existePersona = await _shopContext.Personas.FirstOrDefaultAsync( x => x.Email.Contains(payload.Email));

                if (existePersona == null)
                {
                    throw new ExceptionResponse("No existe persona registrada con ese correo");
                }

                Usuario? existeUsuario = await _shopContext.Usuarios.FirstOrDefaultAsync( x => x.PersonaId == existePersona.Id);

                if (existeUsuario == null)
                {
                    throw new ExceptionResponse("No existe un usario Creado con este correo");
                }
                return null;
            }
            catch (Exception ex)
            {
                return new Response<string>()
                {
                    Message = ex.Message,
                    Data = null,
                    Code = HttpStatusCode.InternalServerError
                };
            }
        }
        public async Task<Response<string>> RegisterService(RegisterDTO payload) 
        {
            try
            {
                Persona? existePersona = await _shopContext.Personas.FirstOrDefaultAsync(x => x.Identificacion.Contains(payload.Identificacion));
                if (existePersona == null) 
                {
                    throw new ExceptionResponse("Este persona no se encuentra registrada en el sistema");
                }

                Usuario nuevoUsuario = new()
                {
                    Password = Encrypter.HashPassword(payload.Password),
                    Username = payload.Username,
                    PersonaId = existePersona.Id,
                };

                var usuarioCreado = await _shopContext.Usuarios.AddAsync(nuevoUsuario);


                return new Response<string>()
                {
                    Code = HttpStatusCode.Created,
                    Data = null,
                    Message = $"Usuario creado con id: {usuarioCreado.Entity.Id}"
                };

            }
            catch (Exception ex)
            {
                return new Response<string>()
                {
                    Code = HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                    Data = null
                };
            }
        }
    }
}
