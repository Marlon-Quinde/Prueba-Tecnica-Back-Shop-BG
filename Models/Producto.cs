using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public partial class Producto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Nombre { get; set; }

    [Required]
    public int Stock { get; set; }

    [Required]
    public double Precio { get; set; }

    [Required]
    public int IdCategoria { get; set; }

    [Required]
    public bool estado { get; set; }
    public Categoria Categoria { get; set; }
}
