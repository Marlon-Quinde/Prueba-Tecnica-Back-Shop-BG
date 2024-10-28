using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO.Auth
{
    public class RegisterDTO
    {
        public string Username { get; set; }
        public string Email { get; set; } 
        public string Password { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateOnly FechaNacimiento { get; set; }
    }
}
