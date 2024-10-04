using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Usuarios
    {
        public int Id_Usuario {  get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Id_Persona { get; set; }


    }
}
