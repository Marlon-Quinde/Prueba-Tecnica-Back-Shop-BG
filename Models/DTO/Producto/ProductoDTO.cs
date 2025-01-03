﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class ProductoDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public int Stock { get; set; }

        public decimal Precio { get; set; }

        public bool Estado { get; set; }

        public string Categoria { get; set; }
    }
}
