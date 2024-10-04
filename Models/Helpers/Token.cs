using Microsoft.IdentityModel.Tokens;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Models.Helpers
{
    public static class Token
    {
        static JwtSecurityTokenHandler TokenHandler = new JwtSecurityTokenHandler();
        public static string GenerateToken(Usuarios user)
        {
            byte[] SecurityKey = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("Token_Key"));
            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id_Persona.ToString()),
                    }
                    ),
                Expires = DateTime.UtcNow.AddHours(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(SecurityKey), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = TokenHandler.CreateToken(TokenDescriptor);
            return TokenHandler.WriteToken(token);
        }
    }
}
