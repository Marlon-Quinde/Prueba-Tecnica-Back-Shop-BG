using Helpers;
using Models.DTO;
using Models.DTO.Producto;

namespace Services
{
    public interface IProductoService
    {
        Task<Response<string>> CrearProductoService(CrearProductoDTO producto);
        Task<List<ProductoDTO>> ObtenerProductosService(string nombre, bool estado);
        Task<Response<string>> ActualizarProductoService(int idProducto, ActualizarProductoDTO payload);
        Task<Response<string>> EliminacionFisicaProductoServices(int id);
    }
}