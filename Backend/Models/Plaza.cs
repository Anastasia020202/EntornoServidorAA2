namespace ParkingApp2.Models
{
    public class Plaza
    {
        public int Id { get; set; }
        public string Numero { get; set; } = "";
        public string Tipo { get; set; } = ""; 
        public bool Disponible { get; set; } = true;
        public decimal PrecioHora { get; set; }
        public DateTime FechaAlta { get; set; } = DateTime.UtcNow;
        public bool Activa { get; set; } = true;
        
        public List<Reserva>? Reservas { get; set; }
    }
}
