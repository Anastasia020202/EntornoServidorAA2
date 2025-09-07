namespace ParkingApp2.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Correo { get; set; } = "";
        public string HashContrasena { get; set; } = "";
        public byte[] SaltContrasena { get; set; } = Array.Empty<byte>();
        public string Rol { get; set; } = "User";
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public bool Activo { get; set; } = true;
        
        public List<Reserva>? Reservas { get; set; }
        public List<Vehiculo>? Vehiculos { get; set; }
    }
}
