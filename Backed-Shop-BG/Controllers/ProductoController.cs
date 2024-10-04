using Business.Producto;
using Microsoft.AspNetCore.Mvc;
using Models.Responses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backed_Shop_BG.Controllers
{
    [Route("api/productos")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        Response response = new Response();
        private readonly IProductoBusiness _productoBussiness;
        public ProductoController(IProductoBusiness productoBusiness)
        {
            this._productoBussiness = productoBusiness;              
        }
        [HttpGet("listado-producto")]
        public async Task<IActionResult> GetProductos()
        {
            try
            {
                response = await _productoBussiness.GetAllProducts();
            }
            catch (Exception ex)
            {
                response.Code = "00";
                response.Data = null;
                response.Message = "Producto Controller / GetProductos / "+ex.Message;
            }
            return Ok(response);
        }
    }
}
