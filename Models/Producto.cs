﻿using System;
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
    [Column(TypeName = "decimal(18,2)")]
    public decimal Precio { get; set; }

    [Required]
    public bool estado { get; set; } = true;

    [Required]
    public int CategoriaId { get; set; }
    public Categoria Categoria { get; set; }
}
