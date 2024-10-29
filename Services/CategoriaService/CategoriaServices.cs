using Helpers;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContext;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Models.DTO.Categoria;

namespace Services.CategoriaService
{
    public class CategoriaServices : ICategoriaServices
    {
        private readonly ShopContext _shopContext;
        public CategoriaServices(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }
        public async Task<Response<List<CategoriaDTO>>> ObtenerTodasLasCategoriasService(string? nombre, bool estado = true)
        {
            try
            {
                var query = _shopContext.Categorias.Where(x => x.Estado == estado);

                if (nombre != null)
                {
                    query = query.Where(x => x.Nombre.Contains(nombre));
                }

                List<CategoriaDTO> categorias = await query
                    .Select( dto => new CategoriaDTO()
                    {
                        Estado = estado,
                        Id = dto.Id,
                        Nombre = dto.Nombre
                    }).ToListAsync();
                return new Response<List<CategoriaDTO>>()
                {
                    Code = HttpStatusCode.OK,
                    Data = categorias,
                    Message = "Transsación Exitosa"
                };
            }
            catch (Exception ex)
            {
                return new Response<List<CategoriaDTO>>()
                {
                    Code = HttpStatusCode.InternalServerError,
                    Data = null,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<string>> CrearCategoriaService(CrearCategoriaDTO payload)
        {
            try
            {
                var existeCategoria = await _shopContext.Categorias.FirstOrDefaultAsync( x => x.Nombre.Contains(payload.Nombre));
                if (existeCategoria != null)
                {
                    throw new ExceptionResponse($"Ya existe una categoria con el nombre {payload.Nombre}");
                }

                Categoria categoria = new()
                {
                    Nombre = payload.Nombre,
                    Estado = payload.Estado,
                };

                var nuevaCategoria = await _shopContext.Categorias.AddAsync(categoria);
                await _shopContext.SaveChangesAsync();

                return new Response<string>()
                {
                    Code = HttpStatusCode.Created,
                    Data = null,
                    Message = $"Se creo la categoria {nuevaCategoria.Entity.Nombre}"
                };
            }
            catch (Exception ex)
            {
                return new Response<string>()
                {
                    Message = ex.Message,
                    Code = HttpStatusCode.InternalServerError,
                    Data = null,
                };
            }
        }
    }
}
