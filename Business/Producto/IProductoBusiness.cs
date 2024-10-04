using Models.Responses;

namespace Business.Producto
{
    public interface IProductoBusiness
    {
        Task<Response> GetAllProducts();
    }
}