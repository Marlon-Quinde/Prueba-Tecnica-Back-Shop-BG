using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapperDemo.Producto
{
    public class ProductoMapper: Profile
    {
        public ProductoMapper()
        {
            // Definir el mapeo entre dos clases
            CreateMap<Producto, ProductoDto>();
            // Otros mapeos que necesites
        }
    }
}
