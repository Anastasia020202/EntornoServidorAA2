using Microsoft.EntityFrameworkCore;
using ParkingApp2.Models;

namespace ParkingApp2.Data
{
    public class ParkingDbContext : DbContext
    {
        public ParkingDbContext(DbContextOptions<ParkingDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Plaza> Plazas { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Reserva> Reservas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar relación Usuario -> Vehiculos
            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Vehiculos)
                .WithOne(v => v.Usuario)
                .HasForeignKey(v => v.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configurar relación Usuario -> Reservas
            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Reservas)
                .WithOne(r => r.Usuario)
                .HasForeignKey(r => r.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configurar relación Vehiculo -> Reservas
            modelBuilder.Entity<Vehiculo>()
                .HasMany(v => v.Reservas)
                .WithOne(r => r.Vehiculo)
                .HasForeignKey(r => r.VehiculoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configurar relación Plaza -> Reservas
            modelBuilder.Entity<Plaza>()
                .HasMany(p => p.Reservas)
                .WithOne(r => r.Plaza)
                .HasForeignKey(r => r.PlazaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Crear admin por defecto
            SeedDefaultAdmin(modelBuilder);
        }

        private void SeedDefaultAdmin(ModelBuilder modelBuilder)
        {
            // Admin por defecto: admin@parking.com / admin123
            // Hash y salt estáticos para evitar cambios en el modelo
            var hash = "Zs37u7MYrBsWJk4BoZ9b/jTYIoLQBWquKmbCkxoO7MM=";
            var salt = Convert.FromBase64String("AfyZXFBOpYP6bqnk3dDZCyc67L5lHYoCiP6KQffR13YPHpW8YZCblSbEzyEQixh6auWLl40vP6HRSyPXK5UwxQ==");
            
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = 1,
                    Correo = "admin@parking.com",
                    HashContrasena = hash,
                    SaltContrasena = salt,
                    Rol = Roles.Admin,
                    FechaCreacion = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    Activo = true
                }
            );
        }

    }
}
