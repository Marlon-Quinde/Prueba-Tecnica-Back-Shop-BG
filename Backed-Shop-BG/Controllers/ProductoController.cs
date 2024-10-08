using Microsoft.AspNetCore.Mvc;
using Helpers;
using Interfaces;
using Models;
using System.Net;
using Models.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backed_Shop_BG.Controllers
{
    [Route("api/productos")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;
        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet("listado-producto")]
        public async Task<IActionResult> GetProductos()
        {
            try
            {
                var listadoProductos = await _productoService.ObtenerProductosServices();
                return Ok(
                    new Response<List<Producto>>()
                    { 
                        Code=HttpStatusCode.OK,
                        Data = listadoProductos,
                        Message = "Consulta Exitosa"
                        
                    }
                    );
            }
            catch (Exception ex)
            {
                return BadRequest(
                    new Response<string>()
                    {
                        Code = HttpStatusCode.InternalServerError,
                        Message = ex.Message,
                        Data = null
                    }
                    );
 
            }
        }
    }
}
