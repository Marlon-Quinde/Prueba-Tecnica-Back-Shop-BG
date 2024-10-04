
using Mappings.Persona;
using Models.Entities;
using Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.PersonaBusiness
{
    public class PersonaBusiness : IPersonaBusiness
    {
        private readonly IPersonaMapping _personaMapping;
        public PersonaBusiness(IPersonaMapping personaMapping)
        {
            this._personaMapping = personaMapping;
        }
        public async Task<Response> CrearPersona(Personas persona)
        {
            Response response = new Response();
            try
            {
                response = await _personaMapping.RegistrarPersona(persona);
            }
            catch (Exception ex)
            {
                response.Code = "01";
                response.Message = "IPersonaMapping / CrearPersona / " + ex.Message;
                response.Data = null;
            }

            return response;
        }
    }
}
