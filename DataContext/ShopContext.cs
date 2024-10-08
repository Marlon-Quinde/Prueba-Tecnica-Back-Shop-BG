using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataContext;

public partial class ShopContext : DbContext
{

    public ShopContext(DbContextOptions<ShopContext> options) : base(options)
    {
    }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>()
            .HasOne(u => u.Persona)
            .WithOne(p => p.Usuario) 
            .HasForeignKey<Usuario>(u => u.IdPersona);
    }



}
