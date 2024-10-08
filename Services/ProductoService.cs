using DataContext;
using Helpers;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductoService : IProductoService
    {
        private readonly ShopContext _dbContext;
        public ProductoService(ShopContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<List<Producto>> ObtenerProductosServices()
        {
            var env = Environment.GetEnvironmentVariable("ConnectionString");
            List<Producto> listproducto = await
                _dbContext.Productos
                .Join(_dbContext.Categoria,
                    p => p.IdCategoria,
                    c => c.Id,
                    (p, c) => new { Producto = p, Categoria = c })
                .Select(dto => new Producto()
                {
                    Nombre = dto.Producto.Nombre,
                    Precio = dto.Producto.Precio,
                    Id = dto.Producto.Id,
                    Categoria = dto.Categoria
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
    }
}
