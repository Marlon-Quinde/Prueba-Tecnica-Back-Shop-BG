using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public partial class Persona
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Identificacion { get; set; }

    [Required]
    public string Nombres { get; set; }

    [Required]
    public string Apellidos { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public DateOnly FechaNacimiento { get; set; }

    public Usuario Usuario { get; set; }
}
