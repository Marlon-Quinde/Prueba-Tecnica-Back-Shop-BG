using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataContext;
using Models.Entities;
using Helpers;
using Services.CategoriaService;
using Models.DTO.Categoria;

namespace Backed_Shop_BG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ShopContext _context;
        private readonly ICategoriaServices _categoriaServices;

        public CategoriasController(ShopContext context, ICategoriaServices categoriaServices)
        {
            _context = context;
            _categoriaServices = categoriaServices;
        }

        // GET: api/Categorias
        [HttpGet]
        public async Task<IActionResult> GetCategorias([FromQuery] string? nombre, [FromQuery] string? estado = "A")
        {
            try
            {
                var response = await _categoriaServices.ObtenerTodasLasCategoriasService(nombre, estado == "A");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<string>()
                {
                    Code = HttpStatusCode.InternalServerError,
                    Data = null,
                    Message = ex.Message
                });
            }
        }

        // GET: api/Categorias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetCategoria(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return categoria;
        }

        // PUT: api/Categorias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria(int id, Categoria categoria)
        {
            if (id != categoria.Id)
            {
                return BadRequest();
            }

            _context.Entry(categoria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Categorias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostCategoria(CrearCategoriaDTO payload)
        {
            try
            {
                var response = await _categoriaServices.CrearCategoriaService(payload);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(
                    new Response<string>()
                    {
                        Message = ex.Message,
                        Code = HttpStatusCode.InternalServerError,
                        Data = null
                    }
                    );
            }
        }

        // DELETE: api/Categorias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoriaExists(int id)
        {
            return _context.Categorias.Any(e => e.Id == id);
        }
    }
}
