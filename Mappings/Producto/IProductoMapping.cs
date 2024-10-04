using Models.Responses;

namespace Mappings.Producto
{
    public interface IProductoMapping
    {
        Task<Response> GetProducts();
    }
}