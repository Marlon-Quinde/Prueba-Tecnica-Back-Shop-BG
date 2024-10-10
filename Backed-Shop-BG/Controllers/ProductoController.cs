using Microsoft.AspNetCore.Mvc;
using Helpers;
using Models;
using System.Net;
using Models.DTO.Producto;
using Services;

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
                    new Response<List<ProductoDTO>>()
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


        [HttpPost("crear")]
        public async Task<IActionResult> CrearProducto(CrearProductoDTO producto) 
        {
            try
            {
                var response = await _productoService.CrearProductoServices(producto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<string>() { 
                    Code = HttpStatusCode.InternalServerError,
                    Data = null,
                    Message = ex.Message
                }
                );
            }
        }
    }
}
