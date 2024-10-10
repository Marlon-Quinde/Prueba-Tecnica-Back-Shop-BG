using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO.Categoria
{
    public class CategoriaDTO { 
        public int Id { get; set; }

        public string Nombre { get; set; }

        public bool Estado { get; set; }
    }
}
