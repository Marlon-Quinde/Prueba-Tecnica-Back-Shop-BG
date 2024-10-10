using Helpers;
using Models.DTO;
using Models.DTO.Producto;

namespace Services
{
    public interface IProductoService
    {
        Task<Response<string>> CrearProductoServices(CrearProductoDTO producto);
        Task<List<ProductoDTO>> ObtenerProductosServices();
    }
}