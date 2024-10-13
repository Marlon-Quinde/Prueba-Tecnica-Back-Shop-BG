using AutoMapper;
using Models;
using Models.DTO;
using Models.DTO.Producto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapperDemo
{
    internal class ProductoMapper: Profile
    {
        public ProductoMapper()
        {
            // Definir el mapeo entre ProductoEntity y ProductoDto
            CreateMap<Producto, ActualizarProductoDTO>();
            CreateMap<ActualizarProductoDTO, Producto>();
        }
    }
}
