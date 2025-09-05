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
        }
    }
}
