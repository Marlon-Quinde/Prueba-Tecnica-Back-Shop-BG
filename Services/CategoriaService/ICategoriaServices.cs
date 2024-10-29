using Helpers;
using Models.DTO.Categoria;

namespace Services.CategoriaService
{
    public interface ICategoriaServices
    {
        Task<Response<string>> CrearCategoriaService(CrearCategoriaDTO payload);
        Task<Response<List<CategoriaDTO>>> ObtenerTodasLasCategoriasService(string? nombre, bool estado = true);
    }
}