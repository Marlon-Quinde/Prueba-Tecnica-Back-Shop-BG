using Models.Entities;
using Models.Responses;

namespace Business.PersonaBusiness
{
    public interface IPersonaBusiness
    {
        Task<Response> CrearPersona(Personas persona);
    }
}