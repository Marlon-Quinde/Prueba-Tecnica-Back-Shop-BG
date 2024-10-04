using Mappings.Producto;
using Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Producto
{
    public class ProductoBusiness : IProductoBusiness
    {
        private readonly IProductoMapping _productoMapping;
        public ProductoBusiness(IProductoMapping productoMapping)
        {
            this._productoMapping = productoMapping;
        }

        public async Task<Response> GetAllProducts()
        {
            Response response = new Response();
            try
            {
                response = await _productoMapping.GetProducts();
            }
            catch (Exception ex)
            {

                response.Code = "01";
                response.Message = "ProductoBusiness / GetAllProducts / " + ex.Message;
                response.Data = null;

            }
            return response;
        }
    }
}
