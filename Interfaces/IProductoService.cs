﻿using Models;
using Models.DTO.Producto;

namespace Interfaces
{
    public interface IProductoService
    {
        Task<List<ProductoDTO>> ObtenerProductosServices();
    }
}