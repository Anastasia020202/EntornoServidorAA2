using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ParkingApp2.Data;
using ParkingApp2.Data.Repositories;
using ParkingApp2.Business.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar EF Core con BBDD en memoria
builder.Services.AddDbContext<ParkingDbContext>(options =>
    options.UseInMemoryDatabase("ParkingDb"));

// Registrar repositorios
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IPlazaRepository, PlazaRepository>();
builder.Services.AddScoped<IVehiculoRepository, VehiculoRepository>();
builder.Services.AddScoped<IReservaRepository, ReservaRepository>();

// Registrar servicios de negocio
builder.Services.AddScoped<AuthService>();

// Configurar JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "ParkingApp2",
            ValidAudience = "ParkingApp2",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("tu-clave-secreta-super-segura-de-al-menos-32-caracteres"))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Migraciones automáticas para BBDD en memoria
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ParkingDbContext>();
    db.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
