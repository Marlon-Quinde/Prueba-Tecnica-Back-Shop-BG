using Models.Queries;
using Models.Responses;

namespace Business.Usuario
{
    public interface IUsuarioBusiness
    {
        Task<Response> LoginUsuarioBusiness(Login query);
        Task<Response> RegistarUsuarioBusiness(PersonaUsuarioQuery query);
    }
}