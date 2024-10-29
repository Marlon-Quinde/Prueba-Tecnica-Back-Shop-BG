using Microsoft.AspNetCore.Mvc;
using Helpers;
using Models;
using System.Net;
using Models.DTO.Producto;
using Services;
using Models.DTO;
using Services.ProductoService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backed_Shop_BG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoServices _productoService;
        public ProductosController(IProductoServices productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductos([FromQuery] string? nombre, [FromQuery] string? estado = "A")
        {
            try
            {
                var listadoProductos = await _productoService.ObtenerProductosService(nombre, estado == "A");
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


        [HttpPost]
        public async Task<IActionResult> CrearProducto(CrearProductoDTO producto) 
        {
            try
            {
                var response = await _productoService.CrearProductoService(producto);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProductoController(int id, ActualizarProductoDTO payload)
        {
            try
            {
                var response = await _productoService.ActualizarProductoService(id, payload);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<string>()
                {
                    Code = HttpStatusCode.InternalServerError,
                    Data = null,
                    Message = ex.Message
                }
                );
            }
        }
    }
}
