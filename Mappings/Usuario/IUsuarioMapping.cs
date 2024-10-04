using Models.Entities;
using Models.Queries;
using Models.Responses;

namespace Mappings.Usuario
{
    public interface IUsuarioMapping
    {
        Task<Response> BuscarUsuarioMapping(Login login);
        Task<Response> RegistrarUsuarioMapping(Usuarios usuario);
    }
}