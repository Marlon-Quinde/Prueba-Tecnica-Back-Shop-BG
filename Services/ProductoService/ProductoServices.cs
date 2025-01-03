﻿using AutoMapper;
using DataContext;
using Helpers;
using Microsoft.EntityFrameworkCore;
using Models.DTO;
using Models.DTO.Categoria;
using Models.DTO.Producto;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.ProductoService
{
    public class ProductoServices : IProductoServices
    {
        private readonly ShopContext _dbContext;
        private readonly IMapper _mapper;
        public ProductoServices(ShopContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<List<ProductoDTO>> ObtenerProductosService(string? nombre, bool estado = true)
        {
            var query = _dbContext.Productos
                .Join(_dbContext.Categorias,
                    p => p.Categoria.Id,
                    c => c.Id,
                    (p, c) => new { Producto = p, Categoria = c })
                .Select(dto => new ProductoDTO()
                {
                    Nombre = dto.Producto.Nombre,
                    Precio = dto.Producto.Precio,
                    Id = dto.Producto.Id,
                    Stock = dto.Producto.Stock,
                    Categoria = dto.Categoria.Nombre,
                    Estado = dto.Producto.Estado
                }
                ).Where(x => x.Estado == estado); ;

            if (nombre != null) {
                nombre = nombre.Trim().ToLower();
                query = query.Where(x => EF.Functions.Like(x.Nombre.Trim().ToLower(), $"%{nombre}%"));
            }


            List<ProductoDTO> listproducto = await query.ToListAsync();
            return listproducto;
        }
        public async Task<Response<string>> CrearProductoService(CrearProductoDTO payload)
        {
            try
            {
                var idProducto = payload.CategoriaId;
                var categoria = await _dbContext.Categorias.Select(
                    cat => new CategoriaDTO()
                    {
                        Id = cat.Id,
                        Estado = cat.Estado,
                        Nombre = cat.Nombre,
                    }
                    ).FirstOrDefaultAsync(p => p.Id == idProducto);

                if (categoria == null)
                {
                    throw new ExceptionResponse("No se encontro la categoria asignada");
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

        public async Task<Response<string>> ActualizarProductoService(int idProducto, ActualizarProductoDTO payload)
        {
            try
            {
                Producto? producto = await _dbContext.Productos.FirstOrDefaultAsync(x => x.Id == idProducto);

                if (producto == null)
                {
                    throw new ExceptionResponse($"No existe el producto con id: {idProducto}");
                }

                _mapper.Map(payload, producto);
                //producto.Stock = payload.Stock;
                //producto.Precio = payload.Precio;
                //producto.Nombre = payload.Nombre;
                //producto.CategoriaId = payload.CategoriaId;
                //_mapperProducto.productotodto(producto, payload);
                //producto = _mapperProducto.productotodto(payload);


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

        public async Task<Response<string>> EliminacionFisicaProductoServices(int id)
        {
            try
            {
                var producto = await _dbContext.Productos.FirstOrDefaultAsync(x => x.Id == id);

                if (producto == null)
                {
                    throw new ExceptionResponse($"No existe el producto con el id: {id}");
                }

                var productoEliminado = _dbContext.Productos.Remove(producto);

                return new Response<string>()
                {
                    Code = HttpStatusCode.OK,
                    Message = "Producto Eliminado con exito",
                    Data = productoEliminado.Entity.Id.ToString()
                };
            }
            catch (Exception ex)
            {
                return new Response<string>()
                {
                    Code= HttpStatusCode.InternalServerError,
                    Message= ex.Message,
                    Data = null
                };
            }
        }


    }

}
