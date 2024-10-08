using Models;
using Models.DTO;

namespace Interfaces
{
    public interface IProductoService
    {
        Task<List<Producto>> ObtenerProductosServices();
    }
}