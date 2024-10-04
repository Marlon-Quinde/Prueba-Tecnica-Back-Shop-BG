using Models.Entities;
using Models.Responses;

namespace Mappings.Persona
{
    public interface IPersonaMapping
    {
        Task<Response> RegistrarPersona(Personas persona);
    }
}