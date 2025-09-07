namespace ParkingApp2.Models
{
    public class Vehiculo
    {
        public int Id { get; set; }
        public string Matricula { get; set; } = "";
        public string Marca { get; set; } = "";
        public string Modelo { get; set; } = "";
        public int UsuarioId { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
        public decimal Kilometros { get; set; } = 0;
        
        public Usuario? Usuario { get; set; }
        public List<Reserva>? Reservas { get; set; }
    }
}
