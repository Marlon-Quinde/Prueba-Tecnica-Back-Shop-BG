using DataContext;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(cors => cors.AddPolicy("AllowWebApp", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().WithExposedHeaders("Content-Disposition")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ? Context
builder.Services.AddDbContext<ShopContext>(options => options.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionString")));

// ? Inyección de Dependencia
// ! Producto
builder.Services.AddScoped<IProductoService, ProductoService>();



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
