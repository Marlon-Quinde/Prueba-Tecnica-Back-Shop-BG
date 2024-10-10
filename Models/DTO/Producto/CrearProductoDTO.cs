using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO.Producto
{
    public class CrearProductoDTO
    {

        public string Nombre { get; set; }

        public int Stock { get; set; } = 0;

        public double Precio { get; set; }

        public bool Estado { get; set; } = true;

        public int CategoriaId { get; set; }
    }
}
