using DataContext;
using Helpers;
using Microsoft.EntityFrameworkCore;
using Models.DTO.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.Auth
{
    public class AuthService
    {
        private readonly ShopContext _shopContext;
        public AuthService(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }
        public Task<Response<string>> LoginService() 
        {

            return null;
        }
        public async Task<Response<string>> RegisterService(RegisterDTO payload) 
        {
            try
            {
                var existePersona = await _shopContext.Personas.FirstOrDefaultAsync( x => x.Email.Contains(payload.Email));
                if (existePersona == null) 
                {
                    throw new ExceptionResponse("Este persona no se encuentra registrada en el sistema");
                }
                return null;

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
