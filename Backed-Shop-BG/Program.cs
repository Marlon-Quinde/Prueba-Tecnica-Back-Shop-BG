using Business.PersonaBusiness;
using Business.Producto;
using Business.Usuario;
using Mappings.Persona;
using Mappings.Producto;
using Mappings.Usuario;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(cors => cors.AddPolicy("AllowWebApp", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().WithExposedHeaders("Content-Disposition")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ? Inyección de Dependencia
// ! Producto
builder.Services.AddScoped<IProductoMapping, ProductoMapping>();
builder.Services.AddScoped<IProductoBusiness, ProductoBusiness>();

// ! Usuario
builder.Services.AddScoped<IUsuarioMapping, UsuarioMapping>();
builder.Services.AddScoped<IUsuarioBusiness, UsuarioBusiness>();

// ! Persona
builder.Services.AddScoped<IPersonaMapping, PersonaMapping>();
builder.Services.AddScoped<IPersonaBusiness, PersonaBusiness>();



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
