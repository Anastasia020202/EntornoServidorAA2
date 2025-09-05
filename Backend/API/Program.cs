using Microsoft.EntityFrameworkCore;
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

// Registrar servicios de negocio
builder.Services.AddScoped<AuthService>();

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
app.MapControllers();

app.Run();
