using AutoMapper;
using DataContext;
using Helpers;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;
using Models.DTO.Categoria;
using Models.DTO.Producto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

// namespace Services
{
    public class ProductoService : IProductoService
    {
        private readonly ShopContext _dbContext;
        private readonly IMapper _mapper;
        public ProductoService(ShopContext dbContext, IMapper mapper)
        // {
            this._dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<List<ProductoDTO>> ObtenerProductosServices()
        {
            List<ProductoDTO> listproducto = await
                _dbContext.Productos
                .Join(_dbContext.Categoria,
                    p => p.Categoria.Id,
                    c => c.Id,
                    (p, c) => new { Producto = p, Categoria = c })
                .Select(dto => new ProductoDTO()
                {
                    Nombre = dto.Producto.Nombre,
                    Precio = dto.Producto.Precio,
                    Id = dto.Producto.Id,
                    Stock = dto.Producto.Stock,
                    Categoria = dto.Categoria.Nombre
                }
                )
                .ToListAsync();

            //List<ProductoDTO> listProductoDTO = new List<ProductoDTO>();
            //foreach (Producto producto in listproducto)
            //{
            //    ProductoDTO productoDTO = new ProductoDTO();
            //    productoDTO.Nombre = producto.Nombre;
            //    productoDTO.Precio = producto.Precio;
            //    productoDTO.Id = producto.Id;


            //        var categoria = _dbContext.Categoria
            //            .Where(a => a.Id == producto.IdCategoria)
            //            .Select(a => a.Nombre)
            //            .ToList();
            //    productoDTO.Categoria = categoria.First();

            //    listProductoDTO.Add(productoDTO);
            //}

            return listproducto;
        }
        public async Task<Response<string>> CrearProductoServices(CrearProductoDTO payload)
        {
            try
            {
                var idProducto = payload.CategoriaId;
                var categoria = await _dbContext.Categoria.Select(
                    cat => new CategoriaDTO()
                    {
                        Id = cat.Id,
                        Estado = cat.Estado,
                        Nombre = cat.Nombre,
                    }
                    ).FirstOrDefaultAsync(p => p.Id == idProducto);

                if (categoria == null)
                {
                    return new Response<string>()
                    {
                        Code = HttpStatusCode.BadRequest,
                        Data = null,
                        Message = "No se encontro el registro"
                    };
                }

                Producto productoNuevo = new Producto()
                {
                    Stock = payload.Stock,
                    CategoriaId = payload.CategoriaId,
                    Nombre = payload.Nombre,
                    Precio = payload.Precio,
                };
                var productoAgregado = await _dbContext.Productos.AddAsync(productoNuevo);

                await _dbContext.SaveChangesAsync();

                return new Response<string>()
                {
                    Code = HttpStatusCode.OK,
                    Data = productoAgregado.Entity.Id.ToString(),
                    Message = "Producto registrado con exito"
                };


            }
            catch (Exception ex)
            {
                return new Response<string>()
                {
                    Code = HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                    Data = null
                };

            }
        }

        public async Task<Response<string>> ActualizarProductoServices(int idProducto, ActualizarProductoDTO payload)
        {
            try
            {
                var producto = await _dbContext.Productos.FirstOrDefaultAsync(x => x.Id == idProducto);

                if (producto == null)
                {
                    return new Response<string>()
                    {
                        Code = HttpStatusCode.BadRequest,
                        Message = $"No existe el producto con id: {idProducto}",
                        Data = null
                    };
                }

                //producto.Stock = payload.Stock;
                //producto.Precio = payload.Precio;
                //producto.Nombre = payload.Nombre;
                //producto.CategoriaId = payload.CategoriaId;
                _mapper.Map(payload, producto);


                var productoActualizado = await _dbContext.SaveChangesAsync();


                return new Response<string>()
                {
                    Code = HttpStatusCode.OK,
                    Data = $"Producto {productoActualizado}",
                    Message = "Producto Actualizado con exito"
                };
            }
            catch (Exception ex)
            {
                return new Response<string>()
                {
                    Code = HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                    Data = null
                };
            }
        }


    }

}
