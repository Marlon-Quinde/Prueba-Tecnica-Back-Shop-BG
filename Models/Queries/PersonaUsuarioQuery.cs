using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Queries
{
    public class PersonaUsuarioQuery
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? Id_Persona { get; set; }
    }

}
