using AutoMapperDemo;
using DataContext;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.CategoriaService;
using Services.ProductoService;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(cors => cors.AddPolicy("AllowWebApp", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().WithExposedHeaders("Content-Disposition")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ? Context
builder.Services.AddDbContext<ShopContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ? AutoMapper
builder.Services.AddAutoMapper(typeof(ProductoMapper));

// ? Inyección de Dependencia
// ! Producto
builder.Services.AddScoped<IProductoServices, ProductoServices>();
builder.Services.AddScoped<ICategoriaServices, CategoriaServices>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowWebApp");

app.Run();
